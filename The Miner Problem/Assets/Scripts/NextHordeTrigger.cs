using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextHordeTrigger : MonoBehaviour
{
    #region Singleton
	public static NextHordeTrigger instance;

	void Awake() {
		if(instance != null) {
			Debug.LogWarning ("More than one instance of NextHordeTrigger found!");
			return;
		}
		instance = this;
	}
	#endregion

    public bool waitingForPlayer;
    private BoxCollider2D boxCollider;
    public Animator bottomRocks;

    public GameObject downArrow;
    public Animator downArrowAnimator;

    // Level layout and its showing order.
    public List<GameObject> levelLayouts;
    private int currLayoutIndex;
    private Queue<int> layoutOrder = new Queue<int>();

    void Start()
    {
        /* Disable portal, disable teleporting and enable bottom rocks. */
        downArrow.SetActive(true);
        downArrowAnimator.SetBool("isActive", false);
        waitingForPlayer = false;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        bottomRocks.SetBool("isActive", true);

        /* Initialize level layout order queue in asceding order */
        for (int i = 1; i < levelLayouts.Count; i++)
            layoutOrder.Enqueue(i);

        currLayoutIndex = 0;
        layoutOrder.Enqueue(0);
        levelLayouts[0].SetActive(true);     

    }

    public void StartNextHordeTrigger()
    {
        /* Activate trigger boolean, activate teleporting, explode rocks, activate portal and activate downArrow indicator. */
        HordeManager.instance.nextHordeTriggerOn = true;
        waitingForPlayer = true;
        bottomRocks.SetBool("isActive", false);
        boxCollider.enabled = true;        
        downArrowAnimator.SetBool("isActive", true);
    }

    public void EndNextHordeTrigger()
    {
        /* Disable trigger boolean, portal and downArrow indicator. */
        HordeManager.instance.nextHordeTriggerOn = false;
    }

    private IEnumerator SetupEndNextHordeTrigger(float timeToKnowNewLevel)
    {
        yield return new WaitForSeconds(timeToKnowNewLevel);
        EndNextHordeTrigger();
    }

    public void OnTriggerPortal ()
    {
        if (!waitingForPlayer)
            return;

        waitingForPlayer = true;

        deleteAllActiveJewels();

        IEnumerator coroutine;

        SetupNextLevelLayout();
        TeleportPlayer();

        bottomRocks.SetBool("isActive", true);
        boxCollider.enabled = false;
        downArrowAnimator.SetBool("isActive", false);

        coroutine = SetupEndNextHordeTrigger(3.0f);
        StartCoroutine (coroutine);        
    }

    private void deleteAllActiveJewels ()
    
    {
        GameObject[] activeJewels = GameObject.FindGameObjectsWithTag("Jewel");

        foreach(GameObject jewel in activeJewels) {
            Destroy(jewel);
        }
    }

    private void SetupNextLevelLayout ()
    {
        /* Gets index of next level layout in queue front, loads it, assigns it to current layout and Enqueue it. */
        int nextLevelLayoutIndex = layoutOrder.Dequeue();

        LoadLevelLayout (nextLevelLayoutIndex);

        currLayoutIndex = nextLevelLayoutIndex;

        layoutOrder.Enqueue(currLayoutIndex);
    }   

    private void LoadLevelLayout (int index)
    {
        /* Disable current level layout and activate next. */
        levelLayouts[currLayoutIndex].SetActive(false);
        levelLayouts[index].SetActive(true);
    }

    private void TeleportPlayer ()
    {
        /* Change player positions to top mid (out of screen). */
        Vector3 newPlayerPos = new Vector3();
        newPlayerPos = Player.instance.transform.position;
        newPlayerPos.x = 0f;
        newPlayerPos.y = 10f;
        
        Player.instance.transform.position = newPlayerPos;
    }
}
