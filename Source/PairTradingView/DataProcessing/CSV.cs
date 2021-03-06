﻿#region LICENSE
//Copyright (c) 2015 Denis V Lebedev

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace PairTradingView.DataProcessing
{
    public class CsvFormat
    {
        public bool ContainsHeader { get; set; }
        public char Separator { get; set; }
        public long PriceIndex { get; set; }
        public long VolumeIndex { get; set; }
    }

    public class Value
    {
        public double Price { get; set; }
        public long Volume { get; set; }
    }

    public class CsvFile
    {
        public static List<Value> Read(string path, CsvFormat format)
        {
            List<Value> result = null;

            try
            {
                result = new List<Value>();

                string[] lines = File.ReadAllLines(path);

                int i = format.ContainsHeader ? 1 : 0;

                for (i = 0; i < lines.Length; i++)
                {
                    string[] cuts = lines[i].Split(new[] { format.Separator }, StringSplitOptions.RemoveEmptyEntries);

                    result.Add(new Value
                    {
                        Price = double.Parse(cuts[format.PriceIndex], CultureInfo.InvariantCulture),

                        Volume = long.Parse(cuts[format.VolumeIndex], CultureInfo.InvariantCulture)
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return result;
        }
    }
}
