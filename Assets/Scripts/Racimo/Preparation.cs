namespace Racimo
{
    public partial class Bouquet
    {
        public VaseType GetVaseType()
        {
            return vase.GetVase().GetVaseType();
        }
    }
}