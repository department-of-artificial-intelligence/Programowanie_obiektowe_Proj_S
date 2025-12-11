namespace Project.Model;

public interface IPlayManager
{
    bool AddPlay(Play play);
    bool RemovePlay(Play play);
    bool RemovePlay(int playId);
    void RemoveAllPlays();
    string GetPlaysString();
}
