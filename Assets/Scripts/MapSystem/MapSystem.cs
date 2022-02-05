using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    //�C�x���g�����ʒu
    [SerializeField] GameObject[] eventOccurrence;

    //�ꏊ���Ƃɔ����ł���C�x���g
    string[][] eventPlaceNum;

    int count;            //��������C�x���g�̐�
    int[] randEventPos;   //��������C�x���g�̏ꏊ(��̃I�u�W�F�N�g�̔ԍ�)

    /// /// /// /// /// /// /// 
    //�g���Ă��邩�ǂ����m�F
    bool[] isUsed;

    public static bool spawnEvents = false;

    // Start is called before the first frame update
    void Start()
    {
        //�������̏�����
        eventPlaceNum = new string[eventOccurrence.Length][];
        isUsed = new bool[eventOccurrence.Length];
        for (int i = 0; i <eventPlaceNum.Length; ++i)
        {
            eventPlaceNum[i] = new string[eventOccurrence[i].GetComponent<EventPositionProperty>().eventNum.Length];
            eventPlaceNum[i] = eventOccurrence[i].GetComponent<EventPositionProperty>().eventNum;
            isUsed[i] = eventOccurrence[i].GetComponent<EventPositionProperty>().isUsed;
        }
    }

    public void Update()
    {
        if (!spawnEvents)
            return;

        SpawnEvents();
    }

    public void SpawnEvents()
    {
        for (int i = 0; i < Parser.eventIsOn.Length; i++)
        {
            if (Parser.eventIsOn[i])
                RandomFunction(Parser.id[i]);
        }
    }


    /// /// /// /// /// /// /// /// /// /// /// 
    //ID�ɍ��킹�Ĉʒu���o�͂���֐�
    private Transform RandomFunction(string id)
    {
        //ID�ɍ��킹���ʒu��ۑ�����
        List<Transform> transforms = new List<Transform>();
        //���X�g����K���Ȉʒu��I�ԁF
        for(int i = 0; i<eventOccurrence.Length; i++)
        {
            //�����C�x���g�͂��̈ʒu���g���Ă��Ȃ�
            if (!isUsed[i])
            {
                //���̈ʒu�ɂ͎������̃C�x���gID���������
                for(int j = 0; j< eventPlaceNum[i].Length; j++)
                {
                    if (id == eventPlaceNum[i][j])
                    {
                        //�����ł���΁A���̈ʒu�����X�g�ɒǉ����܂�
                        transforms.Add(eventOccurrence[i].transform);
                        break;
                    }
                }
            }
        }
        //���X�g�ɂ͓����Ă���ʒu�̐��̊m�F
        int membersAmount = transforms.Count;
        //�������X�g�͋���ۂł͂Ȃ���
        if (membersAmount != 0)
        {
            //���̃��X�g���烉���_���ʒu��I���
            int chosenPosition = Random.Range(0, membersAmount);
            Debug.Log("���X�g�ɂ͓����Ă���A�C�e���̐� "�@+ membersAmount);
            //�g���Ă���t���O��ݒ肵�܂�
            isUsed[chosenPosition] = true;
            Debug.Log("ID " + id + "�C�x���g��" + transforms[chosenPosition] + "�ɔz�u���܂���");
            //�v���O�����Ɉʒu��Ԃ�
            return transforms[chosenPosition];
        }
        //�����łȂ����
        else
        {
            //�C�x���g��z�u�ł��Ȃ������Əo�͂��܂�
            Debug.Log("ID�@" + id + " �C�x���g��z�u�ł��܂���ł���");
            return default;
        }
    }
    /// /// /// /// /// /// /// /// /// /// /// /// 
}