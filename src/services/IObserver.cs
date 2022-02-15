
using Fleck;

namespace LocalChat
{
    interface IObserver<T>
    {
        void Update(T data);
    }
}