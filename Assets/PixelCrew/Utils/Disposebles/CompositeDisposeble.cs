using System;
using System.Collections.Generic;

namespace PixelCrew.Utils.Disposebles
{
    public class CompositeDisposeble : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Retain(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables) { disposable.Dispose(); }
            _disposables.Clear();
        }
    }
}