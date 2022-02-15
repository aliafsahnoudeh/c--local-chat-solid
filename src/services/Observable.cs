
using System.Collections.Generic;

namespace LocalChat
{
    class Observable<T> : IObservable<T>
    {
        #region private fields
        private List<IObserver<T>> observers = new List<IObserver<T>>();
        #endregion

        #region public methods
        public void Attach(IObserver<T> observer)
        {
            observers.Add(observer);
        }
        public void Detach(IObserver<T> observer)
        {
            observers.Remove(observer);
        }
        public void Notify(T data)
        {
            foreach (IObserver<T> observer in observers)
            {
                observer.Update(data);
            }
        }
        #endregion
    }
}
