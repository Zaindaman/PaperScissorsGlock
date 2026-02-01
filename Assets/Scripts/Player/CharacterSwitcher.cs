using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class CharacterSwitcher : MonoBehaviour
{
    public enum CharacterSelected
    {
        Paper,
        Scissors,
        Rock,
        Glock
    }

    [Header("Character Selection")]
    [SerializeField] CharacterSelected selectedCharacter;
    public GameObject CurrentCharacter => currentCharacter;
    public CharacterSelected CurrentCharacterType => selectedCharacter;
    private GameObject currentCharacter;

    [Header("Character Prefabs")]
    [SerializeField] GameObject paperPrefab;
    [SerializeField] GameObject scissorsPrefab;
    [SerializeField] GameObject rockPrefab;
    [SerializeField] GameObject glockPrefab;

    [Header("Player Controls")]
    //for the sake of testing
    [SerializeField] KeyCode paperKey;
    [SerializeField] KeyCode scissorsKey;
    [SerializeField] KeyCode rockKey;
    [SerializeField] KeyCode glockKey;

    //actual keybinds
    [SerializeField] KeyCode attackKey;
    [SerializeField] KeyCode jumpKey;
    [SerializeField] LayerMask playerLayer;

    [Header("Enemy Player")]
    [SerializeField] CharacterSwitcher otherPlayerSwitcher;

    [Header("PlayerData")]
    [SerializeField] Color playerTint = Color.white;

    //Reads the selected characters from the RPSManager and Spawns them in the level.
    
    void Start()
    {
        if (RPSManager.instance != null)
        {
            if (CompareTag("Player1"))
                selectedCharacter = RPSManager.instance.player1Character;
            else if (CompareTag("Player2"))
                selectedCharacter = RPSManager.instance.player2Character;
        }

        SpawnCharacter(selectedCharacter);

    }

    // you may use for testing. Just manually changes the character for debugging
    void Update()
    {
        // Debug keys for switching characters manually
        if (Input.GetKeyDown(paperKey)) SpawnCharacter(CharacterSelected.Paper);
        if (Input.GetKeyDown(scissorsKey)) SpawnCharacter(CharacterSelected.Scissors);
        if (Input.GetKeyDown(rockKey)) SpawnCharacter(CharacterSelected.Rock);
        if (Input.GetKeyDown(glockKey)) SpawnCharacter(CharacterSelected.Glock);
    }


    // Instantiates the correct character prefab, Sets player-specific properties, initialises physics and other objects

    public void SpawnCharacter(CharacterSelected character)
    {
        selectedCharacter = character; 

        if (currentCharacter != null)
            Destroy(currentCharacter);

        GameObject prefabToSpawn = character switch
        {
            CharacterSelected.Paper => paperPrefab,
            CharacterSelected.Scissors => scissorsPrefab,
            CharacterSelected.Rock => rockPrefab,
            CharacterSelected.Glock => glockPrefab,
            _ => null
        };

        if (prefabToSpawn == null)
            return;

        currentCharacter = Instantiate(prefabToSpawn, transform.position, Quaternion.Euler(0, 0, 170), transform);

        var sprite = currentCharacter.transform.Find("Sprite").GetComponent<SpriteRenderer>();

        if (sprite != null)
            sprite.color = playerTint;



        var physics = currentCharacter.GetComponent<PlayerPhysics>();
        {
            physics.jumpKey = jumpKey;
            physics.otherPlayerSwitcher = otherPlayerSwitcher;
        }


        var attackComponents = currentCharacter.GetComponents<PlayerAttack>();
        foreach (var attack in attackComponents)
        {
            var weaponEmpty = currentCharacter.transform.Find("PaperWeapon").gameObject;
            attack.Initialize(attackKey, weaponEmpty, playerLayer);
        }
    }

    // if called it just destroys the character and reinstinates it at the spawnpoint
    public void Respawn()
    {
        if (currentCharacter != null)
            Destroy(currentCharacter);

        transform.position = transform.position;
        SpawnCharacter(selectedCharacter);
    }

}
