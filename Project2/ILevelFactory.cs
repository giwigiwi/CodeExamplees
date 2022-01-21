namespace WayOfLove
{
    public interface ILevelFactory
    {
        Level Create(int index);
        void Load();
    }
}