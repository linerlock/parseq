﻿/*
 * Copyright (C) 2012 - 2015 Takahisa Watanabe <linerlock@outlook.com> All rights reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 * 
 */
using System;

namespace Parseq
{
    public interface IDelayed<out T>
    {
        T Force();
    }

    public partial class Delayed
    {
        public static IDelayed<T> Return<T>(T value)
        {
            return new Delayed.ValueImpl<T>(value);
        }

        public static IDelayed<T> Return<T>(Func<T> valueFactory)
        {
            return new Delayed.ValueFactoryImpl<T>(valueFactory);
        }
    }

    public partial class Delayed
    {
        class ValueImpl<T>
            : IDelayed<T>
        {
            public T Value
            {
                get;
                private set;
            }

            public ValueImpl(T value)
            {
                this.Value = value;
            }

            public T Force()
            {
                return this.Value;
            }
        }

        class ValueFactoryImpl<T>
            : IDelayed<T>
        {
            public System.Lazy<T> DelyedValue
            {
                get;
                private set;
            }

            public ValueFactoryImpl(Func<T> valueFactory)
            {
                this.DelyedValue = new Lazy<T>(valueFactory);
            }

            public T Force()
            {
                return this.DelyedValue.Value;
            }
        }
    }
}
