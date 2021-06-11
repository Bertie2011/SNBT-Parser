using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Bertie.SNBT.Parser.NBT {
    public class NbtCompound : NbtTag, IEnumerable<KeyValuePair<string, NbtTag>> {
        private Dictionary<string, NbtTag> Items { get; } = new Dictionary<string, NbtTag>();

        public ICollection<string> Keys => Items.Keys;

        public ICollection<NbtTag> Values => throw new NotImplementedException();

        public int Count => Items.Count;

        
        public void Add(string key, NbtTag tag) {
            Items.Add(key, tag);
        }

        public void Clear() {
            Items.Clear();
        }

        public bool ContainsKey(string key) {
            return Items.ContainsKey(key);
        }

        public bool Remove(string key) {
            return Items.Remove(key);
        }

        public R ItemAs<R>(string key) where R : NbtTag {
            return Items[key].As<R>();
        }

        public bool TryItemAs<R>(string key, out R value) where R : NbtTag {
            value = default;
            return Items.TryGetValue(key, out var tag) && tag.TryAs(out value);
        }

        public R ValueAs<R>(string key) {
            return Items[key].As<NbtPrimitive>().ValueAs<R>();
        }

        public bool TryValueAs<R>(string key, out R value) {
            value = default;
            return Items.TryGetValue(key, out var tag) && tag.TryAs<NbtPrimitive>(out var primitive) && primitive.TryValueAs(out value);
        }

        public IEnumerator<KeyValuePair<string, NbtTag>> GetEnumerator() {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Items.GetEnumerator();
        }
    }
}
