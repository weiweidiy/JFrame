namespace JFramework.Common.Interface
{
    public interface ISerializer
    {
        string ToJson(object obj);

        T ToObject<T>(string str);
    }
}
