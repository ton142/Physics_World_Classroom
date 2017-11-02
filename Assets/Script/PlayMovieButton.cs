namespace VRTK.NewScripts
{
    using UnityEngine;
    using System.Collections.Generic;
	using UnityEngine.Video;

    public class PlayMovieButton : VRTK_InteractableObject
    {
        public GameObject screen;
        public Material black;
		private VideoPlayer movie;

        private int isPlaying = 0;
        private int firstPlay = 0;

        public override void StartUsing(GameObject usingObject)
        {
            base.StartUsing(usingObject);
            if (firstPlay == 0)
            {
                firstPlay = 1;
				movie = screen.GetComponent<VideoPlayer> ();
				movie.Prepare ();
            }
            PlayMovie();
        }

        private void PlayMovie()
        {
            if (movie != null && isPlaying == 0)
            {
                movie.Play(); // plays the video
                isPlaying = 1;
            } else if (movie != null)
            {
				movie.Pause ();
                isPlaying = 0;
            }
        }
    }
}