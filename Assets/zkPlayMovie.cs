using UnityEngine;
using System.Collections;

public class zkPlayMovie : MonoBehaviour
{
    public bool loop = false;

    MovieTexture movie;
    AudioSource audioTrack;

    // Use this for initialization
    void Start()
    {


        Renderer r = GetComponent<Renderer>();
        movie = (MovieTexture)r.material.mainTexture;
        audioTrack = gameObject.GetComponent<AudioSource>();
        movie.loop = loop;
        audioTrack.loop = loop;

        movie.Stop();
        audioTrack.Stop();

    }


    
	
    public void pauseContinue()
    {
        movie.loop = loop;
        audioTrack.loop = loop;

        if (movie.isPlaying)
        {
            movie.Pause();
            audioTrack.Pause();
        } else
        {
            movie.Play();
            audioTrack.Play();

        }
    }


    

    public void play(float state)
    {
        movie.loop = loop;
        audioTrack.loop = loop;

        if (state > 0)
        {
            movie.Play();
            audioTrack.Play();
        }
        else
        {
            movie.Stop();
            audioTrack.Stop();
        }

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (movie.isPlaying)
            {
                movie.Pause();
                audioTrack.Pause();
            } else
            {
                movie.Play();
                audioTrack.Play();

            }
        }
        if (Input.GetKey(KeyCode.Return))
        {
           movie.Stop();
           audioTrack.Stop();

        }
    }
}


