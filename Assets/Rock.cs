using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Timers;
using TMPro;

public class Rock : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    private RawImage _rawImage;


    private List<Texture2D> firstDestroyFrame = new List<Texture2D>();
    private List<Texture2D> secondDestroyFrame = new List<Texture2D>();
    private List<Texture2D> thirdDestroyFrame = new List<Texture2D>();

    private float framePerSecond = 2.5f;

    public HealBar healBar;
    public Inventory inventory;
    public GameObject storage;

    private RectTransform rectTransform;
    public GameObject ResourceInventoryPrefabItem;
    public InformatePlayer informate;
    public InformatePlayer informate1;
    public InformatePlayer informate2;
    public InformatePlayer informate3;
    public InformatePlayer informate4;

    private int minValue;
    private int maxValue;

    private bool firstDestroy = false;
    private bool secondDestroy = false;
    private bool thirdDestroy = false;

    private float delay = 2.5f;
    //Спустя сколько секунд появится новый камень
   
    public AudioClip soundHit;
    public AudioClip soundDestroy;
    private AudioSource[] audioSource;

    private string[] SpriteFolder = System.IO.Directory.GetDirectories("Assets/Image/Sprite Rock");
    void Start()
    {
        
        minValue = 10;
        //Начальное минимальное значение здоровья
        maxValue = 17;
        //Начальное максимальное значение здоровья

        audioSource = gameObject.GetComponentsInChildren<AudioSource>();
        audioSource[0].clip = soundHit;
        audioSource[1].clip = soundDestroy;

        _rawImage = GetComponent<RawImage>();
        _rawImage = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        giveItem(1000,1);
        newRock(); 
}

    
    List<byte[]> firstFileData = new List<byte[]>();
    List<byte[]> secondFileData = new List<byte[]>();
    List<byte[]> thirdFileData = new List<byte[]>();
    
    int offAnim = 0;
    void Update() {
        //Анимации
         if(thirdDestroy) {
            float index = Time.time * framePerSecond;
            index = index % thirdDestroyFrame.Count;
            _rawImage.texture = thirdDestroyFrame[(int)index];
            if((int)index >= 4) offAnim++;
            if(offAnim >= 128) {thirdDestroy = false; offAnim = 0;return;}
        }
        if(secondDestroy) {
            float index = Time.time * framePerSecond;
            index = index % secondDestroyFrame.Count;
            _rawImage.texture = secondDestroyFrame[(int)index];
            if((int)index >= 4) offAnim++;
            if(offAnim >= 128) {secondDestroy = false; offAnim = 0;return;}
        }
       
        if(firstDestroy) {
            float index = Time.time * framePerSecond;
            index = index % firstDestroyFrame.Count;
            _rawImage.texture = firstDestroyFrame[(int)index];
            if((int)index >= 4) offAnim++;
            if(offAnim >= 128) {firstDestroy = false; offAnim = 0;return;}
        }
        if(Input.GetKeyUp(KeyCode.Q)) {
           giveItem(1000,0);
           giveItem(1000,1);
           giveItem(1000,2);
           giveItem(1000,3);
           giveItem(1000,4);
        }
    }



    private int countItem = 0;
    private int indexItem;
    private string nameItem;

    private void giveItem(int _countItem, int _indexItem) {
        if(_countItem <= 0) return;
        string imageResource = _indexItem +1 + ".png";
        Debug.Log(imageResource);
        // Debug.Log(imageResource);
        string[] ResourcesFiles = System.IO.Directory.GetFiles("Assets/Image/Resources",imageResource);
        //Ищет файл картинки
        string path = ResourcesFiles[0];


        if(_indexItem == 0) nameItem = "blue";
        if(_indexItem == 1) nameItem = "red";
        if(_indexItem == 2) nameItem = "gold";
        if(_indexItem == 3) nameItem = "green";
        if(_indexItem == 4) nameItem = "violet";
        inventory.AddItem("resource",path,nameItem,_countItem,0,0,0,0,0);
        
    }


    public int luck = 0;

    private void newRock() {
        // informate.infoPlayer("Камень создался");
        //Необходимо для правильного воспроизведения анимации

        firstDestroyFrame.Clear();
        secondDestroyFrame.Clear();
        thirdDestroyFrame.Clear();
        firstFileData.Clear();
        secondFileData.Clear();
        thirdFileData.Clear();
        //Очищает предыдущие анимации

        maxHealth = RandomNumber(minValue,maxValue);
        //Назначает случайное число в диапозоне от минимального и максимального значения
        
        countItem = Mathf.RoundToInt(maxHealth/70f*RandomNumber(2,7)+RandomNumber(0,2));
        
        //Количество выпадаемых предметов зависит от максимального хп

        healBar.SetMaxHealth(maxHealth);
        //Передаём в хил бар максимальное здоровье
        currentHealth = maxHealth;
        //Текущее здоровье равно максимальному
        minValue+=2;
        //Необходимо для увеличения здоровья
        maxValue+=2;
        //Необходимо для увеличения здоровья

        int index = RandomNumber(0,SpriteFolder.Length);
        // informate1.infoPlayer(currentHealth + "");
        // informate2.infoPlayer(maxHealth + "");
        Debug.Log(index);
        indexItem = index;
        //Рандомно выбираем руду
        string[] images = System.IO.Directory.GetFiles(SpriteFolder[index]);
        // informate3.infoPlayer(images[index] + "");


        //Заходим в папку с этой рудой, берём оттуда все файлы
        string[] folder = System.IO.Directory.GetDirectories(SpriteFolder[index]);
        //Заходим в папку с этой рудой, берём оттуда все папки

        string[] frameAnim;
        //В папках хранятся все спрайты необходимые для анимации
        for(int a = 0; a<3; a++) {
            // Так как у нас будет всего 3 стадии разрушения, цикл ограничен до 3
            frameAnim = System.IO.Directory.GetFiles(folder[a]);
            //Заходим в каждую из папок, получаем от них файлы
            for(int i =0;i<frameAnim.Length;i+=2) {
                if(a==0) {
                    firstFileData.Add(System.IO.File.ReadAllBytes(frameAnim[i]));
                    //В динамический массив №1 записываем всё что находится в папке, в байтах, для дальнейшего воспроизведения
                }
                else if(a==1) {
                    secondFileData.Add(System.IO.File.ReadAllBytes(frameAnim[i]));
                    //В динамический массив №2 записываем всё что находится в папке, в байтах, для дальнейшего воспроизведения
                }
                else if(a==2) {
                    thirdFileData.Add(System.IO.File.ReadAllBytes(frameAnim[i]));
                    //В динамический массив №3 записываем всё что находится в папке, в байтах, для дальнейшего воспроизведения
                }
            }
            for(int j = 0;j< 5;j++) {
                Texture2D texture;
                texture = new Texture2D(2, 2);
                if(a==0) {
                    texture.LoadImage(firstFileData[j]);
                    //Грузим текстуру
                    firstDestroyFrame.Add(texture);
                    //Добавляем в динамический массив №1 подгружаемые текстуры
                }
                else if(a==1) {
                    texture.LoadImage(secondFileData[j]);
                    //Грузим текстуру
                    secondDestroyFrame.Add(texture);
                    //Добавляем в динамический массив №1 подгружаемые текстуры
                }
                else if(a==2) {
                    texture.LoadImage(thirdFileData[j]);
                    //Грузим текстуру
                    thirdDestroyFrame.Add(texture);
                    //Добавляем в динамический массив №1 подгружаемые текстуры
                }
            }
        }   


        Texture2D newTexture = new Texture2D(2, 2);
        //Создаём текстуру для подгрузки
        byte[] fileData;
        if(index == 4) {
            fileData = System.IO.File.ReadAllBytes(images[1]);
            Debug.Log("FFFFF");
        } else {
             fileData = System.IO.File.ReadAllBytes(images[3]);
            Debug.Log("AAAA");
            //Под индексом 3 всегда будет картинка начальной руды
        }
        // byte[] fileData = System.IO.File.ReadAllBytes(images[0]);

        newTexture.LoadImage(fileData);
        //Грузим
        _rawImage.texture = newTexture;

        //Подставляем
        // rectTransform.sizeDelta = new Vector2(RandomNumber(77,100), RandomNumber(78,90));
        //Назначает случайную ширину и высоту новой руде
    }
    private int stopAnim  = 0;
    public void changeSpriteRock(float healthValue) {
        audioSource[0].Play();
        if(healthValue <= 0) {
            thirdDestroy = true;
            audioSource[1].Play();
            stopAnim = 0;
            giveItem(countItem,indexItem);
            // informate.infoPlayer("третья анимация проигралась");
            Invoke("newRock", delay);
        }
        else if(healthValue <= 0.45f  && stopAnim==1) {
            secondDestroy = true;
            stopAnim++;
            // informate.infoPlayer("вторая анимация проигралась");
            audioSource[1].Play();

        }else if(healthValue <=0.7f && stopAnim==0) {
            firstDestroy = true;
            stopAnim++;
            audioSource[1].Play();
            // informate.infoPlayer("Первая анимация проигралась");

        } 
        
    }

    public void TakeDamage(int damage) {
        currentHealth-=damage;
        healBar.SetHealth(currentHealth);
        float health = healBar.currentHealth();
        changeSpriteRock(health);
    }


    private int RandomNumber(int min,int max) {
        return Random.Range(min,max);
    }


}
