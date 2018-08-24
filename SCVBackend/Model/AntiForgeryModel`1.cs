namespace SCVBackend.Model
{
    public class AntiForgeryModel<T>
    {
        public string Token { get; set; }
        public T Model { get; set; }
    }
}
