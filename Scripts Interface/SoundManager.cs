using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

public interface ISoundManager
{
    void SoundPlay(string audioName);
    void SoundEffect(string audioName);
}

class SoundManager : ISoundManager
{
    public SoundManager()
    {
        ServiceLocator.RegisterService<ISoundManager>(this);
    }

    public void SoundPlay(string audioName)
    {
        Song song = ServiceLocator.GetService<ContentManager>().Load<Song>("sounds/" + audioName);
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Play(song);
    }

    public void SoundEffect(string audioName)
    {
        SoundEffect soundEffect = ServiceLocator.GetService<ContentManager>().Load<SoundEffect>("sounds/" + audioName);
        soundEffect.Play();
    }

}