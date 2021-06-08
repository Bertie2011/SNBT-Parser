using System;
using System.Collections.Generic;
using System.Text;

namespace Bertie.SNBT.Parser.NBT {
    public abstract class NbtTag {
        public bool Is<T>() where T : NbtTag {
            return this is T;
        }
        public T As<T>() where T : NbtTag {
            return (T)this;
        }
        public bool TryAs<T>(out T result) where T : NbtTag {
            if (Is<T>()) {
                result = As<T>();
                return true;
            } else {
                result = default;
                return false;
            }
        }
    }
}
