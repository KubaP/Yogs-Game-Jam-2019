using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{

    enum State {
        NewPerson,
        PersonComing,
        DocumentComing,
        Inspection,
        DocumentLeaving,
        PersonLeaving,
        End,
        Start,
        Pause
    };

    State state = State.Start;
    State prevState = State.Start;

    public Resources resources;

    public bool personDirection = true;
    public bool forceFake = true;
    public bool currentPersonValid = true;
    public bool visaRequired = false;
    public Person person;
    public int peopleLeft = 30;
    public int maxPeople;
    public float maxTime;
    public float timeLeft;
    public AnimationCurve walkGraph;


    public Date today;


    public int livesLeft;
    public int maxLives;

    public Passport passport;

    public Radio radio;
    float radioMax;


    public Slider masterSlider;
    public AudioSource enterSound;
    public AudioSource exitSound;
    public AudioSource presentSound;

    public TextMesh livesText;
    public TextMesh timeText;
    public TextMesh dateText;
    public Text endText;
    public Text endScore;


    public Visa visa;


    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject creditsMenu;
    public GameObject tutorialMenu;
    public GameObject endMenu;


    public List<PersonData> validPassports;
    public List<VisaData> validVisas;


    public Slider difficultySlider;



    // Start is called before the first frame update
    void Start()
    {
        Reset();
        validPassports = new List<PersonData>();

        for (int i = 0; i < 100; ++i) {
            PersonData data = new PersonData();
            data.GenerateData(i, today);
            validPassports.Add(data);
        }
    }

    void NewPerson(bool valid) {
        peopleLeft--;

        valid = (forceFake)? false : valid;

        currentPersonValid = valid;

        person.SetData(validPassports[Random.Range(0, validPassports.Count)]);
        if (!valid)
            person.personData.ID = -1;

        person.NewPerson();
        person.transform.position = new Vector3(-3.2f, 0.25f, 0.0f);
        float randomScale = Random.Range(1.45f, 1.65f);
        float squashness = Random.Range(0.9f, 1.1f);
        person.transform.localScale = new Vector3(randomScale * squashness, randomScale, 1.0f);

        
        passport.SetData(person.personData);
        passport.SetSprites(person);
        passport.SetDisplay(person);
        passport.transform.position = person.transform.position;
        passport._collider.size = new Vector2(0.2f, 0.25f);
        passport.ClosePassport();
        passport.enabled = false;

        visa.SetData(person.personData, today);
        visa.SetDisplay(person);
        visa.transform.position = person.transform.position;
        visa._collider.size = new Vector2(0.54f, 0.26f);
        visa.CloseVisa();
        visa.enabled = false;

        if (person.personData.ID == -1) {
            switch (Random.Range(0, 6)) {
                case 0:
                case 1:
                case 2:
                    print("False passport");
                    passport.Falsify();
                    passport.SetDisplay(person);
                    break;
                case 3:
                case 4:
                    if (person.personData.nationality != 3) {
                        print("False visa");
                        visa.Falsify();
                        visa.SetDisplay(person);
                    }
                    else {
                        print("False passport");
                        passport.Falsify();
                        passport.SetDisplay(person);
                    }
                    break;
                case 5:
                    print("False both");
                    visa.Falsify();
                    visa.SetDisplay(person);
                    passport.Falsify();
                    passport.SetDisplay(person);
                    break;
            }
        }
        else {
            print("Legit");
        }

        enterSound.Play();

        state = State.PersonComing;
    }

    public void ProcessPerson(bool allowedThrough) {
        if (state == State.Inspection) {
            // Check if the passport is valid
            passport.enabled = false;
            personDirection = allowedThrough;
            passport.ClosePassport();
            state = State.DocumentLeaving;

            visa.enabled = false;
            visa.CloseVisa();
        }
    }

    public void CloseGame() {
        Application.Quit();
    }

    public void StartGame() {
        state = State.NewPerson;
        mainMenu.SetActive(false);
    }

    public void ShowOptions() {
        if (state == State.Start) {
            mainMenu.SetActive(false);
        }
        if (state == State.Pause) {
            pauseMenu.SetActive(false);
        }
        optionsMenu.SetActive(true);
    }

    public void HideOptions() {
        optionsMenu.SetActive(false);
        if (state == State.Start) {
            mainMenu.SetActive(true);
        }
        if (state == State.Pause) {
            pauseMenu.SetActive(true);
        }
    }

    public void ShowCredits() {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideCredits() {
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void PauseGame() {
        prevState = state;
        state = State.Pause;
        passport.ClosePassport();
        visa.CloseVisa();
        pauseMenu.SetActive(true);
    }

    public void ResumeGame() {
        state = prevState;
        pauseMenu.SetActive(false);
    }

    public void ShowTutorial() {
        tutorialMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HideTutorial() {
        tutorialMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void SetMasterVolume() {
        AudioListener.volume = masterSlider.value / 100.0f;
    }

    public void ShowEnd() {
        state = State.End;
        endMenu.SetActive(true);
    }

    public void Restart() {
        radio.intensity = 0.0f;
        endMenu.SetActive(false);
        mainMenu.SetActive(true);
        state = State.Start;
        Reset();
    }

    public void Reset() {
        maxLives = maxPeople / 4;
        maxTime = maxPeople * 20.0f;

        timeLeft = maxTime;
        livesLeft = maxLives;
        peopleLeft = maxPeople;

        today.Randomise(2000, 2020);

        dateText.text = today.LongDate();

        passport.enabled = false;
        visa.enabled = false;
    }

    void MovePassportAway() {
        passport.transform.position = Vector3.Lerp(passport.transform.position, new Vector3(0.8f, 0.75f, -0.2f), Time.deltaTime * 3.0f);
        if (person.personData.nationality != 3) visa.transform.position = Vector3.Lerp(visa.transform.position, new Vector3(0.8f, 0.75f, -0.2f), Time.deltaTime * 3.0f);
        if (Mathf.Abs(0.75f - passport.transform.position.y) < 0.1f) {
            if (Mathf.Abs(0.8f - passport.transform.position.x) < 0.1f) {
                if (Mathf.Abs(0.75f - visa.transform.position.y) < 0.1f || (person.personData.nationality == 3)) {
                    if (Mathf.Abs(0.8f - visa.transform.position.x) < 0.1f || (person.personData.nationality == 3)) {
                        state = State.PersonLeaving;
                        passport.transform.position = new Vector3(-2.6f, 0.0f, -0.2f);
                        visa.transform.position = new Vector3(-2.6f, 0.0f, -0.2f);
                        exitSound.Play();
                        if (!personDirection) {
                            person.Flip();
                            person.transform.position = new Vector3(0.2f, 0.0f, 0.0f);
                        }
                    }
                }
            }
        }
    }

    void MovePersonAway() {
        float mDirection = (personDirection)? 1.0f : -1.0f;
        float newX = person.transform.position.x + mDirection * Time.deltaTime;
        person.transform.position = new Vector3(newX, 0.25f + walkGraph.Evaluate(person.transform.position.x) * 0.1f, 0.0f);

        if (Mathf.Abs(person.transform.position.x) > 3.2f) {
            if (!personDirection)
                person.Flip();
            
            if (currentPersonValid && !personDirection) {
                livesLeft--;
            }
            if (!currentPersonValid && personDirection) {
                livesLeft--;
            }

            state = State.NewPerson;
        }
    }

    void MovePerson() {
        float newX = person.transform.position.x + Time.deltaTime;
        person.transform.position = new Vector3(newX, 0.25f + walkGraph.Evaluate(person.transform.position.x) * 0.1f, 0.0f);

        if (person.transform.position.x > 0.1f) {
            state = State.DocumentComing;
            passport.transform.position = new Vector3(0.8f, 0.65f, -0.2f);
            presentSound.Play();
            if (person.personData.nationality != 3) visa.transform.position = new Vector3(0.8f, 0.65f, -0.2f);
        }
    }

    void PresentDocuments() {
        passport.transform.Translate(0.4f * Time.deltaTime, -1.0f * Time.deltaTime, 0.0f);
        visa.transform.Translate(-0.4f * Time.deltaTime, -1.0f * Time.deltaTime, 0.0f);
        if (passport.transform.position.y < -0.1f) {
            state = State.Inspection;
            passport.enabled = true;
            visa.enabled = true;
        }
    }

    void LoseGame(string message) {
        state = State.End;
        endText.text = "YOUR' FIRED!";
        endScore.text = message;
        radio.intensity = 8.0f;
        ShowEnd();
    }

    void WinGame() {
        state = State.End;
        endText.text = "YOUR' WINN!";
        float peopleScore = 1.0f - ((float) peopleLeft / (float) maxPeople);
        float livesScore = (maxLives==0)? 1.0f : (float) livesLeft / (float) maxLives;
        float timeScore = 250f * (timeLeft / maxTime);
        endScore.text = string.Format("SCORE {0}%", Mathf.Clamp((int)(peopleScore * livesScore * timeScore), 0, 100));
        radio.intensity = 0.0f;
        ShowEnd();
    }

    void UpdateTime() {
        int minutes = (int)((1.0f - (timeLeft / maxTime)) * 360.0f) ;
        int hour = minutes / 60 + 12;
        minutes = minutes % 60;
        timeText.text = string.Format("{0}:{1}", hour, minutes.ToString().PadLeft(2, '0'));
    }

    void SetRadioIntensity() {
        radio.intensity = 
        (1.0f - timeLeft / maxTime) * 2.0f + 
        (1.0f - ((float) livesLeft / (float) maxLives)) * 3.0f +
        (1.0f - ((float) peopleLeft / (float) maxPeople)) * 2.0f;
    }

    void SetStrings() {
        livesText.text = peopleLeft.ToString();
        UpdateTime();
    }

    // Update is called once per frame
    void Update()
    {       
        switch (state) {
            case State.Start:
                // Do nothing
                break;
            case State.NewPerson:
                NewPerson(!(Random.Range(0.0f, 1.0f) < (difficultySlider.value * 0.5f)));
                break;
            case State.PersonComing:
                MovePerson();
                break;
            case State.DocumentComing:
                PresentDocuments();
                break;
            case State.DocumentLeaving:
                MovePassportAway();
                break;
            case State.PersonLeaving:
                MovePersonAway();
                break;
            case State.Inspection:
                // Nothing
                break;
            case State.End:
                // Do nothing
                break;
        }


        if (state != State.Start && state != State.End) {
            if (state != State.Pause) {
                timeLeft -= Time.deltaTime;
                SetStrings();
                SetRadioIntensity();
                if (peopleLeft < 0) {
                    WinGame();
                }
                if (timeLeft < 0) {
                        LoseGame("You failed to complete your shift");
                }
                if (livesLeft == -1) {
                    LoseGame("You failed to perform proper checks");
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (state != State.Pause) {
                    PauseGame();
                }
                else {
                    ResumeGame();
                }
            }
        }

    }
}
