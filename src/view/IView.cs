namespace LocalChat
{
    interface IView
    {
        void Write(string data);
        string Read();
        void Start();
    }
}
