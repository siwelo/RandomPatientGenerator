using System.Collections.Generic;

namespace Clearwave.IO {
    public interface IDelimitedRow : IEnumerable<string> {
        int Length { get; }
        string this[int index] { get; }
    }
}
