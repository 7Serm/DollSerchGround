using Niantic.Lightship.AR.NavigationMesh;
using UnityEngine;

public class NaviMeshManager : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private LightshipNavMeshManager _navmeshManager;

    [SerializeField]
    private LightshipNavMeshAgent _agentPrefab;

    private LightshipNavMeshAgent _agentInstance;

    private Vector3 _prehabposition;

    [SerializeField]
    private GameObject _marker;

    private GameObject _markerOBj;

    void Update()
    {
        if (_markerOBj == null)
        {
            FindObj();
            if(_agentPrefab != null)
            {
                InstatiatePrehab();
            }
        }
        else
        {

            HandleTouch();
        }


    }

    public void SetVisualization(bool isVisualizationOn)
    {
        //�i�r���b�V���̃����_�����O���I�t�ɂ���
        _navmeshManager.GetComponent<LightshipNavMeshRenderer>().enabled = isVisualizationOn;

        if (_agentInstance != null)
        {
            //�A�N�e�B�u�G�[�W�F���g�̃p�X�����_�����O���I�t�ɂ���
            _agentInstance.GetComponent<LightshipNavMeshAgentPathRenderer>().enabled = isVisualizationOn;
        }
    }
    private void FindObj()
    {
        _markerOBj = GameObject.FindWithTag("AgentBase");
        _prehabposition = _markerOBj.GetComponent<Vector3>();
        //var _agentcomp = gobj.GetComponent<LightshipNavMeshAgent>();
        //_agentInstance = _agentcomp;
    }

    private void HandleTouch()
    {
        //�G�f�B�^�ł̓}�E�X�N���b�N���g�p���A�d�b�ł̓^�b�`���g�p���܂��B
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
#else
        var touch = Input.GetTouch(0);

        //�^�b�`���Ȃ����A�^�b�`�� UI �v�f��I�������ꍇ
        if (Input.touchCount <= 0)
            return;
        if (touch.phase == UnityEngine.TouchPhase.Began)
#endif
        {
#if UNITY_EDITOR
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
#else
            Ray ray = _camera.ScreenPointToRay(touch.position);
#endif
            //�^�b�`�|�C���g���X�N���[����Ԃ���3D�ɓ��e���A������G�[�W�F���g�փf�X�e�B�l�[�V�����Ƃ��ēn��
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (_agentInstance == null)
                {
                    /*_agentInstance = Instantiate(_agentPrefab,_prehabposition,Quaternion.identity);
                   _agentInstance.transform.position = hit.point;*/
                }
                else
                {
                    _agentInstance.SetDestination(hit.point);
                }
            }
        }
    }


    private void InstatiatePrehab()
    {
        if (_agentInstance == null)
        {
            _agentInstance = Instantiate(_agentPrefab, _prehabposition, Quaternion.identity);
        }
    }
}
