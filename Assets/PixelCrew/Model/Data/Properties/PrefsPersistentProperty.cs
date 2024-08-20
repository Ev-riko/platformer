namespace PixelCrew.Model.Data.Properties
{
    public abstract class PrefsPersistentProperty<TpropertyType> : PersistentProperty<TpropertyType>
    {
        protected string Key;

        protected PrefsPersistentProperty(TpropertyType defaultvalue, string key) : base(defaultvalue)
        {
            Key = key;
        }
    }
}
