using UnityEngine;

public class RadioStations : MonoBehaviour
{
    [SerializeField] AudioClip[] songs = new AudioClip[4];
    public static bool isTakePill, isShowingPosters, isShowingShapes, isAfterFirstSong, isAfterSecondSong = false;
    AudioSource aS;
    RadioInteractScript rIS;

    //Mirror components
    [SerializeField] GameObject mirror, elderKey; 
    [SerializeField] ParticleSystem fog;

    //Shape puzzle components
    [SerializeField] GameObject[] childPuzzle = new GameObject[4];
    [SerializeField] GameObject[] puzzleSlots = new GameObject[3];
    Vector3[] shapesLocs = new Vector3[4];

    void Start()
    {
        aS = GetComponent<AudioSource>();
        rIS = GetComponent<RadioInteractScript>();
        mirror.SetActive(false);
        for (int _shapeIndex = 0; _shapeIndex < childPuzzle.Length; _shapeIndex++) // Hides all components of childs puzzle in elder room
        {
            childPuzzle[_shapeIndex].SetActive(false);
            shapesLocs[_shapeIndex] = childPuzzle[_shapeIndex].transform.position;
        }
    }

    void Update()
    {
        if (isTakePill)
        {
            if (!aS.isPlaying) aS.Play();

            if (rIS.GetKnobRotation() > 110 && rIS.GetKnobRotation() < 130 && !isAfterFirstSong && !isAfterSecondSong)
            {
                aS.clip = songs[1]; // plays song from adult room
                mirror.SetActive(true);
                ParticleSystem.MainModule mainModule = fog.main;
                mainModule.maxParticles = 10000;
            }

            else if (rIS.GetKnobRotation() > 50 && rIS.GetKnobRotation() < 80 && isAfterFirstSong)
            {
                aS.clip = songs[2]; // plays song from teen room

                if(!isShowingPosters)
                {
                    FindObjectOfType<PosterCont>().ShowPosters(true);
                    isShowingPosters = true;
                }
            }

            else if (rIS.GetKnobRotation() > -80 && rIS.GetKnobRotation() < -30 && isAfterSecondSong)
            {
                aS.clip = songs[3]; // plays song from baby room
                
                if(!isShowingShapes)
                {
                    isShowingShapes = true;
                    foreach(GameObject shape in childPuzzle)
                    {
                        shape.SetActive(true);
                    }
                }
            }

            else
            {
                aS.clip = songs[0];

                //Disable all mirror puzzle components
                mirror.SetActive(false);
                ParticleSystem.MainModule mainModule = fog.main;
                mainModule.maxParticles = 1;
                elderKey.SetActive(false);

                //Disable all posters puzzle components
                isShowingPosters = false;
                FindObjectOfType<PosterCont>().ShowPosters(false);

                //Disable all child puzzle components
                ShapeEngine._correctSlot = 3;
                isShowingShapes = false;

                for (int _shapeIndex = 0; _shapeIndex < childPuzzle.Length; _shapeIndex++)
                {
                    childPuzzle[_shapeIndex].SetActive(false);
                    childPuzzle[_shapeIndex].transform.position = shapesLocs[_shapeIndex];
                }

                foreach(GameObject slot in puzzleSlots)
                {
                    slot.SetActive(false);
                }
            }
        }
    }

    public AudioClip GetCurrentSong()
    {
        return aS.clip;
    }
}

