using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CharacterMotor : MonoBehaviour
{
    // Animations du perso
    Animation animations;

    // Scripts playerinventory
    PlayerInventory playerInv;

    //Vitesse de déplacement du perso
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;

    // Variables concernant l'attaque
    public static float attackCooldown = (1.5f - (UI.DexteriteTotal * 0.003f));
    private bool isAttacking;
    private double currentCooldown;
    public float attackRange;
    public GameObject rayHit;
    public GameObject raySpell;

    AudioSource audios;
    public AudioClip attackPlayer;

    //sorts
    //eclair
    public float eclairSpellCost;
    public GameObject eclairSpellGameObj;
    public float eclairSpeed;
    public GameObject eclairSpellActivation; //spell disponible et activé
    public static bool eclairIsUp = false;
    public static float eclair = 0;
    //maladie
    public float maladieSpellCost;
    public GameObject maladieSpellGameObj;
    public float maladieSpeed;
    public GameObject maladieSpellActivation; //spell disponible et activé
    public static bool maladieIsUp = false;
    public static float maladie = 0;
    //buff hp
    public float BuffHPCost;
    public GameObject BuffHPGameObj;
    public GameObject BuffHPActivation; //spell disponible et activé
    public static bool BuffHPIsUp = false;
    public static float BuffHp = 0;
    //buff mana
    public float BuffManaCost;
    public GameObject BuffManaGameObj;
    public GameObject BuffManaActivation; //spell disponible et activé
    public static bool BuffManaIsUp = false;
    public static float BuffMana = 0;
    //buff des druides
    public float BuffDroodCost;
    public GameObject BuffDroodGameObj;
    public GameObject BuffDroodActivation; //spell disponible et activé
    public static bool BuffDroodIsUp = false;
    public static float BuffDrood = 0;
    //Inputs
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    private GameObject Background;
    public Vector3 jumpSpeed; //vitesse saut

    public GameObject player; // le GO du joueur

    public static bool isDead = false;

    CapsuleCollider playerCollider; //Collision
    void Start()
    {
        audios = GetComponent<AudioSource>();
        animations = gameObject.GetComponent<Animation>();
        playerCollider = gameObject.GetComponent<CapsuleCollider>();
        playerInv = gameObject.GetComponent<PlayerInventory>();
        rayHit = GameObject.Find("RayHit");
        if (PlayerPrefs.GetInt("load") == 1) // Si on reprend la partie
        {
            float x = PlayerPrefs.GetFloat("x");
            float y = PlayerPrefs.GetFloat("y");
            float z = PlayerPrefs.GetFloat("z");
            playerInv.currentHealth = PlayerPrefs.GetFloat("CurrentHp", playerInv.currentHealth);
            playerInv.currentMana = PlayerPrefs.GetFloat("CurrentMana", playerInv.currentMana);
            UI.ForceTotal = PlayerPrefs.GetInt("force", UI.ForceTotal);
            UI.EnduranceTotal = PlayerPrefs.GetInt("Endurance", UI.EnduranceTotal);
            UI.IntelligenceTotal = PlayerPrefs.GetInt("intelligence", UI.IntelligenceTotal);
            UI.CourageTotal = PlayerPrefs.GetInt("courage", UI.CourageTotal);
            UI.SagesseTotal = PlayerPrefs.GetInt("sagesse", UI.SagesseTotal);
            UI.DexteriteTotal = PlayerPrefs.GetInt("dextérité", UI.DexteriteTotal);
            UI.PointsValue = PlayerPrefs.GetInt("pointsDispo", UI.PointsValue);
            PlayerInventory.playerLevel = PlayerPrefs.GetInt("Lvl", PlayerInventory.playerLevel);
            PlayerInventory.currentXp = PlayerPrefs.GetFloat("XP", PlayerInventory.currentXp);
            PlayerInventory.maxXp = PlayerPrefs.GetFloat("maxXp", PlayerInventory.maxXp);
            PlayerInventory.maxHealth = PlayerPrefs.GetFloat("Vie", PlayerInventory.maxHealth);
            PlayerInventory.maxMana = PlayerPrefs.GetFloat("Mana", PlayerInventory.maxMana);
            PlayerInventory.currentDamage = PlayerPrefs.GetFloat("Dégats", PlayerInventory.currentDamage);
            PlayerInventory.currentArmor = PlayerPrefs.GetFloat("Armure", PlayerInventory.currentArmor);
            attackCooldown = PlayerPrefs.GetFloat("Cooldown", attackCooldown);
            eclair = PlayerPrefs.GetFloat("EclairSpeel", eclair);
            maladie = PlayerPrefs.GetFloat("MaladieSpeel", maladie);
            BuffHp = PlayerPrefs.GetFloat("BuffHp", BuffHp);
            BuffMana = PlayerPrefs.GetFloat("BuffMana", BuffMana);
            BuffDrood = PlayerPrefs.GetFloat("BuffDrood", BuffDrood);
            Dialogue1.force1 = PlayerPrefs.GetInt("Force1", Dialogue1.force1);
            Dialogue2.endurance1 = PlayerPrefs.GetInt("Endurance1", Dialogue2.endurance1);
            Dialogue5.dex1 = PlayerPrefs.GetInt("Dex1", Dialogue5.dex1);
            Dialogue5.dex2 = PlayerPrefs.GetInt("Dex2", Dialogue5.dex2);
            EnemyDogChief.courage1 = PlayerPrefs.GetInt("Courage1", EnemyDogChief.courage1);
            EnemyDogChief.courage2 = PlayerPrefs.GetInt("Courage2", EnemyDogChief.courage2);
            Dialogue6.intelligence1 = PlayerPrefs.GetInt("Intelligence1", Dialogue6.intelligence1);
            Dialogue6.sagesse1 = PlayerPrefs.GetInt("Sagesse1", Dialogue6.sagesse1);
            DialogueBlackSmith.endurance2 = PlayerPrefs.GetInt("Endurance2", DialogueBlackSmith.endurance2);
            DialogueBlackSmith.endurance3 = PlayerPrefs.GetInt("Endurance3", DialogueBlackSmith.endurance3);
            DialogueChampion.dexterite3 = PlayerPrefs.GetInt("Dex3", DialogueChampion.dexterite3);
            DialogueChampion.dexterite4 = PlayerPrefs.GetInt("Dex4", DialogueChampion.dexterite4);
            DialogueLadyRuid.intelligence2 = PlayerPrefs.GetInt("Intelligence2", DialogueLadyRuid.intelligence2);
            DialogueLadyRuid.intelligence3 = PlayerPrefs.GetInt("Intelligence3", DialogueLadyRuid.intelligence3);
            DialogueMayor.courage3 = PlayerPrefs.GetInt("Courage3", DialogueMayor.courage3);
            DialogueMayor.courage4 = PlayerPrefs.GetInt("Courage4", DialogueMayor.courage4);
            DialogueTavernier.force2 = PlayerPrefs.GetInt("Force2", DialogueTavernier.force2);
            DialogueTavernier.force3 = PlayerPrefs.GetInt("Force3", DialogueTavernier.force3);
            Dialogue2.XpQuêteLoup = PlayerPrefs.GetInt("WolfQuest", Dialogue2.XpQuêteLoup);
            Dialogue4.XpQuêteSpider = PlayerPrefs.GetInt("SpiderQuest", Dialogue4.XpQuêteSpider);
            Dialogue4.XpQuêteSpiderQueen = PlayerPrefs.GetInt("SpiderQueen", Dialogue4.XpQuêteSpiderQueen);
            Dialogue4.XpQuêteJail = PlayerPrefs.GetInt("Jail", Dialogue4.XpQuêteJail);
            DialogueMerlinramix.XpQuêteHerbes = PlayerPrefs.GetInt("Herbes", DialogueMerlinramix.XpQuêteHerbes);
            DialogueMerlinramix.XpQuêtePotion = PlayerPrefs.GetInt("Chancres", DialogueMerlinramix.XpQuêtePotion);
            DialogueBlackSmith.XpQuêteChienvalier = PlayerPrefs.GetInt("Armures", DialogueBlackSmith.XpQuêteChienvalier);
            DialogueMayor.XpQuêteMayor = PlayerPrefs.GetInt("Maire", DialogueMayor.XpQuêteMayor);
            DialogueDuchelvau.XpQuêteChampion = PlayerPrefs.GetInt("Champion", DialogueDuchelvau.XpQuêteChampion);
            DialogueTavernier.XpQuêteGobelin = PlayerPrefs.GetInt("Gobelins", DialogueTavernier.XpQuêteGobelin);
            DialogueTavernier.XpQuêteLieutenant = PlayerPrefs.GetInt("Lieutenant", DialogueTavernier.XpQuêteLieutenant);

            player.transform.position = new Vector3(x, y, z);
        }
    }
    bool IsGrounded()
        {
            Vector3 dwn = transform.TransformDirection(Vector3.down);

            return (Physics.Raycast(transform.position, dwn, 0.5f));
        }  

    void Update()
    {
        if (!isDead)
        {
            if (Input.GetKey(inputBack))// si on recule
            {
                transform.Translate(0, 0, -(walkSpeed) * Time.deltaTime);
                animations.Play("walk");
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
            }
            if (Input.GetKey(inputLeft))// si on tourne à gauche
            {
                transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);

            }
            if (Input.GetKey(inputRight))// si on tourne à droite
            {
                transform.Rotate(0, turnSpeed * Time.deltaTime, 0);

            }
            if (Input.GetKeyDown(KeyCode.Escape)) //si on veut retourner au menu
            {
                save();
                SceneManager.LoadScene(0);
            }
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift)) //si on avance
            {
                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                if (!isAttacking)
                {
                    animations.Play("walk");
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
            }
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))// si on veut sprint
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                if (!isAttacking)
                {
                    animations.Play("run");
                }
            }
            if (!Input.GetKey(inputFront) && !Input.GetKey(inputBack))// si on n'avance ni ne recule
            {
                if (!isAttacking)
                {
                    animations.Play("idle");
                }
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    attack();
                }
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    attackEclairSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    BuffHpSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    BuffManaSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    attackMaladieSpell();
                }
                if (Input.GetKeyDown(KeyCode.Alpha5))
                {
                    BuffDroodSpell();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) // si on saute
            {
                // Préparation du saut
                Vector3 v = gameObject.GetComponent<Rigidbody>().velocity;
                v.y = jumpSpeed.y;

                //Saut
                gameObject.GetComponent<Rigidbody>().velocity = jumpSpeed;
            }
            if (isAttacking)
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
            if (eclair == 1)
            {
                eclairIsUp = true;
                if (eclairIsUp == true)
                {
                    eclairSpellActivation.SetActive(true);
                }
            }
            if (BuffHp == 1)
            {
                BuffHPIsUp = true;
                if (BuffHPIsUp == true)
                {
                    BuffHPActivation.SetActive(true);
                }
            }
            if (BuffMana ==1)
            {
                BuffManaIsUp = true;
                if (BuffManaIsUp == true)
                {
                    BuffManaActivation.SetActive(true);
                }
            }
            if (BuffDrood ==1)
            {
                BuffDroodIsUp = true;
                if (BuffDroodIsUp == true)
                {
                    BuffDroodActivation.SetActive(true);
                }
            }
            if (maladie ==1)
            {
                maladieIsUp = true;
                if (maladieIsUp == true)
                {
                    maladieSpellActivation.SetActive(true);
                }
            }
        }

    }
    //fonction d'attaque
    public void attack()
    {
        if (!isAttacking)
        {
            animations.Play("attack");
            audios.clip = attackPlayer;
            audios.Play();

            RaycastHit hit;

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyAi>().ApplyDammage(PlayerInventory.currentDamage);
                }
                if (hit.transform.tag == "EnemyWolf")
                {
                    hit.transform.GetComponent<EnemyAiWolf>().ApplyDammage(PlayerInventory.currentDamage);
                }
                if (hit.transform.tag == "EnemySpider")
                {
                    hit.transform.GetComponent<EnemySpider>().ApplyDammage(PlayerInventory.currentDamage);
                }
            }
            isAttacking = true;
        }
    }
    //fonction d'attaque sorts
    public void attackEclairSpell()
    {
        if (!isAttacking && playerInv.currentMana >= eclairSpellCost) //Sort Eclair
        {
            if (eclairIsUp)
            {
                animations.Play("resist");
                GameObject theSpell = Instantiate(eclairSpellGameObj, raySpell.transform.position, transform.rotation);
                theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * eclairSpeed);
                playerInv.currentMana -= eclairSpellCost;
                isAttacking = true;
            }
        }
    }
    public void BuffHpSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffHPCost) 
        {
            if (BuffHPIsUp)
            {
                BuffHPIsUp = false;
                animations.Play("victory");
                PlayerInventory.maxHealth = PlayerInventory.maxHealth * 1.3f;
                StartCoroutine(BuffHpEnd());
                Instantiate(BuffHPGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffHPCost;
            }
        }
    }
    public void BuffManaSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffManaCost)
        {
            if (BuffManaIsUp)
            {
                BuffManaIsUp = false;
                animations.Play("victory");
                PlayerInventory.maxMana = PlayerInventory.maxMana * 1.3f;
                StartCoroutine(BuffManaEnd());
                Instantiate(BuffManaGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffManaCost;
            }
        }
    }
    public void BuffDroodSpell()
    {
        if (!isAttacking && playerInv.currentMana >= BuffDroodCost)
        {
            if (BuffDroodIsUp)
            {
                BuffDroodIsUp = false;
                animations.Play("victory");
                PlayerInventory.currentDamage += 10;
                PlayerInventory.currentArmor += 10;
                StartCoroutine(BuffDroodEnd());
                Instantiate(BuffDroodGameObj, raySpell.transform.position, transform.rotation);
                playerInv.currentMana -= BuffDroodCost;
            }
        }
    }
    public void attackMaladieSpell()
    {
        if (!isAttacking && playerInv.currentMana >= maladieSpellCost) 
        {
            if (maladieIsUp)
            {
                maladieIsUp = false;
                animations.Play("resist");
                GameObject theSpell = Instantiate(maladieSpellGameObj, raySpell.transform.position, transform.rotation);
                theSpell.GetComponent<Rigidbody>().AddForce(transform.forward * maladieSpeed);
                playerInv.currentMana -= maladieSpellCost;
                StartCoroutine(MaladieCDEnd());
                isAttacking = true;
            }
        }
    }
    IEnumerator BuffHpEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffHPIsUp = true;
        PlayerInventory.maxHealth = PlayerInventory.maxHealth / 1.3f;
    }
    IEnumerator BuffManaEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffManaIsUp = true;
        PlayerInventory.maxMana = PlayerInventory.maxMana / 1.3f;
    }
    IEnumerator BuffDroodEnd()
    {
        yield return new WaitForSeconds(300f);
        BuffDroodIsUp = true;
        PlayerInventory.currentDamage -= 10;
        PlayerInventory.currentArmor -= 10;
    }
    IEnumerator MaladieCDEnd()
    {
        yield return new WaitForSeconds(12f);
        maladieIsUp = true;
    }
    //sauvegarde
    public void save()
    {
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
        PlayerPrefs.SetFloat("x", playerCollider.transform.position.x);
        PlayerPrefs.SetFloat("y", playerCollider.transform.position.y);
        PlayerPrefs.SetFloat("z", playerCollider.transform.position.z);
        PlayerPrefs.SetFloat("CurrentHp", playerInv.currentHealth);
        PlayerPrefs.SetFloat("CurrentMana", playerInv.currentMana);
        PlayerPrefs.SetInt("force", UI.ForceTotal);
        PlayerPrefs.SetInt("Endurance", UI.EnduranceTotal);
        PlayerPrefs.SetInt("intelligence", UI.IntelligenceTotal);
        PlayerPrefs.SetInt("courage", UI.CourageTotal);
        PlayerPrefs.SetInt("sagesse", UI.SagesseTotal);
        PlayerPrefs.SetInt("dextérité", UI.DexteriteTotal);
        PlayerPrefs.SetInt("pointsDispo", UI.PointsValue);
        PlayerPrefs.SetInt("Lvl", PlayerInventory.playerLevel);
        PlayerPrefs.SetFloat("XP", PlayerInventory.currentXp);
        PlayerPrefs.SetFloat("maxXp", PlayerInventory.maxXp);
        PlayerPrefs.SetFloat("Vie", PlayerInventory.maxHealth);
        PlayerPrefs.SetFloat("Mana", PlayerInventory.maxMana);
        PlayerPrefs.SetFloat("Dégats", PlayerInventory.currentDamage);
        PlayerPrefs.SetFloat("Armure", PlayerInventory.currentArmor);
        PlayerPrefs.SetFloat("Cooldown", attackCooldown);
        PlayerPrefs.SetFloat("EclairSpeel", eclair);
        PlayerPrefs.SetFloat("MaladieSpeel", maladie);
        PlayerPrefs.SetFloat("BuffHp", BuffHp);
        PlayerPrefs.SetFloat("BuffMana", BuffMana);
        PlayerPrefs.SetFloat("BuffDrood", BuffDrood);
        PlayerPrefs.SetInt("Force1", Dialogue1.force1);
        PlayerPrefs.SetInt("Endurance1", Dialogue2.endurance1);
        PlayerPrefs.SetInt("Dex1", Dialogue5.dex1);
        PlayerPrefs.SetInt("Dex2", Dialogue5.dex2);
        PlayerPrefs.SetInt("Courage1", EnemyDogChief.courage1);
        PlayerPrefs.SetInt("Courage2", EnemyDogChief.courage2);
        PlayerPrefs.SetInt("Intelligence1", Dialogue6.intelligence1);
        PlayerPrefs.SetInt("Sagesse1", Dialogue6.sagesse1);
        PlayerPrefs.SetInt("Endurance2", DialogueBlackSmith.endurance2);
        PlayerPrefs.SetInt("Endurance3", DialogueBlackSmith.endurance3);
        PlayerPrefs.SetInt("Dex3", DialogueChampion.dexterite3);
        PlayerPrefs.SetInt("Dex4", DialogueChampion.dexterite4);
        PlayerPrefs.SetInt("Intelligence2", DialogueLadyRuid.intelligence2);
        PlayerPrefs.SetInt("Intelligence3", DialogueLadyRuid.intelligence3);
        PlayerPrefs.SetInt("Courage3", DialogueMayor.courage3);
        PlayerPrefs.SetInt("Courage4", DialogueMayor.courage4);
        PlayerPrefs.SetInt("Force2", DialogueTavernier.force2);
        PlayerPrefs.SetInt("Force3", DialogueTavernier.force3);
        PlayerPrefs.SetInt("WolfQuest", Dialogue2.XpQuêteLoup);
        PlayerPrefs.SetInt("SpiderQuest", Dialogue4.XpQuêteSpider);
        PlayerPrefs.SetInt("SpiderQueen", Dialogue4.XpQuêteSpiderQueen);
        PlayerPrefs.SetInt("Jail", Dialogue4.XpQuêteJail);
        PlayerPrefs.SetInt("Herbes", DialogueMerlinramix.XpQuêteHerbes);
        PlayerPrefs.SetInt("Chancres", DialogueMerlinramix.XpQuêtePotion);
        PlayerPrefs.SetInt("Armures", DialogueBlackSmith.XpQuêteChienvalier);
        PlayerPrefs.SetInt("Maire", DialogueMayor.XpQuêteMayor);
        PlayerPrefs.SetInt("Champion", DialogueDuchelvau.XpQuêteChampion);
        PlayerPrefs.SetInt("Gobelins", DialogueTavernier.XpQuêteGobelin);
        PlayerPrefs.SetInt("Lieutenant", DialogueTavernier.XpQuêteLieutenant);
    }
}