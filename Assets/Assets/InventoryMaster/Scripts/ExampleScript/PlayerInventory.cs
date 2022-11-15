using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public GameObject inventory;
    public GameObject characterSystem;
    public GameObject craftSystem;
    private Inventory craftSystemInventory;
    private CraftSystem cS;
    private Inventory mainInventory;
    private Inventory characterSystemInventory;
    private Tooltip toolTip;

    private InputManager inputManagerDatabase;

    
    Image hpImage;
    public TextMeshProUGUI Hp;

    Image manaImage;
    public TextMeshProUGUI Mana;

    public static float maxHealth = 100;
    public static float maxMana = 100;
    public float maxDamage = currentDamage;
    public float maxArmor = currentArmor;

    public float currentHealth = 100;
    public float currentMana = 100;
    public static float currentDamage = 10;
    public static float currentArmor = 0;

    public static float pointManaIncreasePersec;
    public static float pointHPIncreasePersec;

    //Expérience
    Image experienceBar;
    TextMeshProUGUI playerLevelText;
    public TextMeshProUGUI Xp;
    static public int playerLevel = 1;
    public static float currentXp = 0;
    public static float maxXp = 200;
    public float rateXp;

    int normalSize = 3;

    public CharacterMotor charactermotor;
    public Animation playerAnimations;

    //sons
    AudioSource audios;
    public AudioClip deathPlayer;
    public AudioClip lvlUp;

    //respawn du joueur
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnPoint;
    public Text mortText;

    void RespawnPoint ()
    {
        transform.position = spawnPoint.position;
    }
    public void OnEnable()
    {
        Inventory.ItemEquip += OnBackpack;
        Inventory.UnEquipItem += UnEquipBackpack;

        Inventory.ItemEquip += OnGearItem;
        Inventory.ItemConsumed += OnConsumeItem;
        Inventory.UnEquipItem += OnUnEquipItem;

        Inventory.ItemEquip += EquipWeapon;
        Inventory.UnEquipItem += UnEquipWeapon;
    }

    public void OnDisable()
    {
        Inventory.ItemEquip -= OnBackpack;
        Inventory.UnEquipItem -= UnEquipBackpack;

        Inventory.ItemEquip -= OnGearItem;
        Inventory.ItemConsumed -= OnConsumeItem;
        Inventory.UnEquipItem -= OnUnEquipItem;

        Inventory.UnEquipItem -= UnEquipWeapon;
        Inventory.ItemEquip -= EquipWeapon;
    }

    void EquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //add the weapon if you unequip the weapon
        }
    }

    void UnEquipWeapon(Item item)
    {
        if (item.itemType == ItemType.Weapon)
        {
            //delete the weapon if you unequip the weapon
        }
    }

    void OnBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
        {
            for (int i = 0; i < item.itemAttributes.Count; i++)
            {
                if (mainInventory == null)
                    mainInventory = inventory.GetComponent<Inventory>();
                mainInventory.sortItems();
                if (item.itemAttributes[i].attributeName == "Slots")
                    changeInventorySize(item.itemAttributes[i].attributeValue);
            }
        }
    }

    void UnEquipBackpack(Item item)
    {
        if (item.itemType == ItemType.Backpack)
            changeInventorySize(normalSize);
    }

    void changeInventorySize(int size)
    {
        dropTheRestItems(size);

        if (mainInventory == null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (size == 3)
        {
            mainInventory.width = 3;
            mainInventory.height = 1;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        if (size == 6)
        {
            mainInventory.width = 3;
            mainInventory.height = 2;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 12)
        {
            mainInventory.width = 4;
            mainInventory.height = 3;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 16)
        {
            mainInventory.width = 4;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
        else if (size == 24)
        {
            mainInventory.width = 6;
            mainInventory.height = 4;
            mainInventory.updateSlotAmount();
            mainInventory.adjustInventorySize();
        }
    }

    void dropTheRestItems(int size)
    {
        if (size < mainInventory.ItemsInInventory.Count)
        {
            for (int i = size; i < mainInventory.ItemsInInventory.Count; i++)
            {
                GameObject dropItem = (GameObject)Instantiate(mainInventory.ItemsInInventory[i].itemModel);
                dropItem.AddComponent<PickUpItem>();
                dropItem.GetComponent<PickUpItem>().item = mainInventory.ItemsInInventory[i];
                dropItem.transform.localPosition = GameObject.FindGameObjectWithTag("Player").transform.localPosition;
            }
        }
    }

    void Start()
    {
        mortText.enabled = false;
        hpImage = GameObject.Find("currentHp").GetComponent<Image>();
        manaImage = GameObject.Find("currentMana").GetComponent<Image>();

        experienceBar = GameObject.Find("currentXp").GetComponent<Image>();
        playerLevelText = GameObject.Find("NiveauValue").GetComponent<TextMeshProUGUI>();

        audios = GetComponent<AudioSource>();

        charactermotor = gameObject.GetComponent<CharacterMotor>();
        playerAnimations = gameObject.GetComponent<Animation>();

        if (inputManagerDatabase == null)
            inputManagerDatabase = (InputManager)Resources.Load("InputManager");

        if (craftSystem != null)
            cS = craftSystem.GetComponent<CraftSystem>();

        if (GameObject.FindGameObjectWithTag("Tooltip") != null)
            toolTip = GameObject.FindGameObjectWithTag("Tooltip").GetComponent<Tooltip>();
        if (inventory != null)
            mainInventory = inventory.GetComponent<Inventory>();
        if (characterSystem != null)
            characterSystemInventory = characterSystem.GetComponent<Inventory>();
        if (craftSystem != null)
            craftSystemInventory = craftSystem.GetComponent<Inventory>();
    }


    public void OnConsumeItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
            {
                if ((currentHealth + item.itemAttributes[i].attributeValue) > maxHealth)
                    currentHealth = maxHealth;
                else
                    currentHealth += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Mana")
            {
                if ((currentMana + item.itemAttributes[i].attributeValue) > maxMana)
                    currentMana = maxMana;
                else
                    currentMana += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Armor")
            {
                if ((currentArmor + item.itemAttributes[i].attributeValue) > maxArmor)
                    currentArmor = maxArmor;
                else
                    currentArmor += item.itemAttributes[i].attributeValue;
            }
            if (item.itemAttributes[i].attributeName == "Damage")
            {
                if ((currentDamage + item.itemAttributes[i].attributeValue) > maxDamage)
                    currentDamage = maxDamage;
                else
                    currentDamage += item.itemAttributes[i].attributeValue;
            }
        }
        
    }

    public void OnGearItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor += item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage += item.itemAttributes[i].attributeValue;
        }
        
        //{
        
        //}
    }

    public void OnUnEquipItem(Item item)
    {
        for (int i = 0; i < item.itemAttributes.Count; i++)
        {
            if (item.itemAttributes[i].attributeName == "Health")
                maxHealth -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Mana")
                maxMana -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Armor")
                maxArmor -= item.itemAttributes[i].attributeValue;
            if (item.itemAttributes[i].attributeName == "Damage")
                maxDamage -= item.itemAttributes[i].attributeValue;
        }
        
    }

    public void ApplyDamage(float TheDamage)
    {
        if (!CharacterMotor.isDead)
        {
            // la fameuse équation : PDV = PDV -(damage - ((armor * damage) / 100))
            currentHealth = currentHealth - (TheDamage - ((currentArmor * TheDamage) / 100));

            if (currentHealth <= 0)
            {
                Dead();
            }
        }
    }

    public void Dead()
    {
        // On désactive la possibilité de déplacer son personnage lorsqu'il meurt
        CharacterMotor.isDead = true;
        playerAnimations.Play("diehard");
        audios.clip = deathPlayer;
        audios.Play();
        mortText.enabled = true;
        StartCoroutine(DeathTime());
    }

    //CoroutineMort
    IEnumerator DeathTime()
    {
        yield return new WaitForSeconds(4f);
        mortText.enabled = false;
        CharacterMotor.isDead = false;
        currentHealth = maxHealth / 3;
        currentMana = maxMana / 3;
        RespawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        pointManaIncreasePersec = ((0.02f * UI.SagesseTotal) + RegenFire.fireRegen);
        pointHPIncreasePersec = ((0.02f * UI.CourageTotal) + RegenFire.fireRegen);
        Hp.text = (int)currentHealth + "/" + maxHealth;
        Mana.text = (int)currentMana + "/" + maxMana;
        Xp.text = (int)currentXp + "/" + (int)maxXp;

        //REGEN
        if ((0 < currentHealth) & (currentHealth < maxHealth))
        {
            currentHealth += pointHPIncreasePersec * Time.deltaTime;
        }
        if (currentHealth >= maxHealth)
        {
            currentHealth = maxHealth;
        }
        if (currentMana < maxMana)
        {
            currentMana += pointManaIncreasePersec * Time.deltaTime;
        }
        if (currentMana >= maxMana)
        {
            currentMana = maxMana;
        }
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        if (currentMana <= 0)
        {
            currentMana = 0;
        }

        //si on a assez d'Xp
        if(currentXp >= maxXp)
        {
            audios.clip = lvlUp;
            audios.Play();
            float reste = currentXp - maxXp;
            playerLevel += 1;
            currentHealth = maxHealth;
            currentMana = maxMana;
            GameManager.messageList.Clear();
            GameManager.PlayerAnswer = "lvlup";
            UI.PointsValue += 10;
            playerLevelText.text = "" + playerLevel;
            currentXp = 0 + reste;
            maxXp = maxXp * rateXp;
        }

        float percentageXp = ((currentXp * 100) / maxXp) / 100; //barre d'expérience
        experienceBar.fillAmount = percentageXp;

        float percentageHp = ((currentHealth * 100) / maxHealth) / 100; //barre de vie
        hpImage.fillAmount = percentageHp;

        float percentageMana = ((currentMana * 100) / maxMana) / 100; //barre de mana
        manaImage.fillAmount = percentageMana;

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!characterSystem.activeSelf)
            {
                characterSystemInventory.closeInventory();
            }
            else
            {
                if (toolTip != null)
                    characterSystemInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.InventoryKeyCode))
        {
            if (!inventory.activeSelf)
            {
                mainInventory.closeInventory();
            }
            else
            {
                if (toolTip != null)
                
                mainInventory.closeInventory();
            }
        }

        if (Input.GetKeyDown(inputManagerDatabase.CraftSystemKeyCode))
        {
            if (!craftSystem.activeSelf)
                craftSystemInventory.closeInventory();
            else
            {
                if (cS != null)
                    cS.backToInventory();
                if (toolTip != null)
                craftSystemInventory.closeInventory();
            }
        }

    }

}
