**This repo has been archived and will not be updated or maintained in any way, the project is still usable however.**

# DarknetDiaries
A small program to play and keep track of [darknet diaries](https://darknetdiaries.com/) episodes.

![preview image of the main screen](https://github.com/nightowl286/DarknetDiaries/raw/master/.screenshots/list.png)

---

It does not save any data about the episodes, nor does it download any of the episodes, it uses the [rss feed](https://feeds.megaphone.fm/darknetdiaries) provided by
Darknet Diaries, and it also streams the episode from the url provided by the rss feed.

The only thing that does end up being saved is where you have left off on each episode (or if you have finished it).

---

It has an internal player, which will turn see-through if you aren't hovering over it (this is to make it less distracting while in use).

![preview image of the player](https://github.com/nightowl286/DarknetDiaries/raw/master/.screenshots/player.png)

---
### Extra info
A known pitfall is that since the internal WPF player is not asynchronous, and it is used to stream the episode, the program might freeze for a moment when you tell
it to play an episode.

### Usage and installation
In order to be able to use the program, the [.NET 6.0 Desktop Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/6.0/runtime) is required.
After that just download the [release](https://github.com/nightowl286/DarknetDiaries/releases/tag/v1.0.0)
([direct link](https://github.com/nightowl286/DarknetDiaries/releases/download/v1.0.0/DarknetDiaries.zip)), extract the folder, and run the .exe file.
