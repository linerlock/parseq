﻿/*
 * Parseq - a monadic parser combinator library for C#
 *
 * Copyright (c) 2012 - 2013 WATANABE TAKAHISA <x.linerlock@gmail.com> All rights reserved.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parseq
{
    public abstract partial class Stream<TToken> 
        : Option<TToken>
        , IComparable<Stream<TToken>>
        , IDisposable
    {
        public abstract Position Position { get; }
        public abstract Boolean CanNext();
        public abstract Boolean CanRewind();
        public abstract Stream<TToken> Next();
        public abstract Stream<TToken> Rewind();
        public abstract void Dispose();
    }

    partial class Stream<TToken>
    {
        public virtual Int32 CompareTo(Stream<TToken> other){
            return (this.Position.Index - other.Position.Index);
        }

        public static Boolean operator >(Stream<TToken> x, Stream<TToken> y){
            return x.Position > y.Position;
        }

        public static Boolean operator >=(Stream<TToken> x, Stream<TToken> y){
            return x.Position >= y.Position;
        }

        public static Boolean operator <(Stream<TToken> x, Stream<TToken> y){
            return x.Position < y.Position;
        }

        public static Boolean operator <=(Stream<TToken> x, Stream<TToken> y){
            return x.Position <= y.Position;
        }

        public static Stream<TToken> operator >>(Stream<TToken> stream, Int32 count){
            return Enumerable
                .Range(1, count)
                .Aggregate(stream, (s, i) => s.Next());
        }

        public static Stream<TToken> operator <<(Stream<TToken> stream, Int32 count){
            return Enumerable
                .Range(1, count)
                .Aggregate(stream, (s, i) => s.Rewind());
        }
    }

}
