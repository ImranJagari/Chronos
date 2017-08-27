namespace Chronos.ORM
{
    public interface ISaveIntercepter
    {
        void BeforeSave(bool insert);
    }
}