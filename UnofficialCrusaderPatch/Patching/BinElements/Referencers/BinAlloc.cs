﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnofficialCrusaderPatch
{
    public class BinAlloc : BinElement, IEnumerable<BinElement>
    {
        string name;
        public string Name => name;

        public BinAlloc(string name, uint size)
            : this(name, new byte[size])
        {
        }

        public BinAlloc(string name, byte[] data)
        {
            this.name = name;

            editData.Add(new BinLabel(name));
            if (data != null && data.Length > 0)
                editData.Add(new BinBytes(data));
            editData.Add(new BinNops(4));
        }

        UCPEdit editData = new UCPEdit();
        public UCPEdit EditData => editData;

        public IEnumerator<BinElement> GetEnumerator() => editData.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => editData.GetEnumerator();

        public virtual void Add(BinElement input)
        {
            // add in front of nops
            editData.Insert(editData.Count - 1, input);
        }
    }
}
