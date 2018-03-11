namespace HitmanModel.Acl
{
    public enum Operation
    {
        None = 0,
        Read = 1 << 1,
        Write = 1 << 2
    }
}
