namespace Emreraslan.Web.Helpers
{
    public static class ValueGenerater
    {
        public static string FileNameGenerater(string fileExtension)
        {
            return Guid.NewGuid().ToString().Replace("-","") + fileExtension;
        }
    }
}
