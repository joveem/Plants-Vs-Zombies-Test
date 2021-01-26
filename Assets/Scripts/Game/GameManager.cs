using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int plantToPlantId = 1, boardWidth = 9, boardHeight = 5;
    public GameObject gameboardPivot, piecePrefab, lastSelectedPiece;
    public bool isPlantingPlant = false, isMovingPlant = false;

    public List<GameObject> gameboardPieces;

    public List<PlantPiece> plants;
    public PlantPiece selectedPlant;

    public Plant[] plantsList;

    // Start is called before the first frame update
    void Start()
    {

        InstantiateGameBoard();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {

            InstantiateGameBoard();

        }

        Ray ray_ = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit_;

        if (Physics.Raycast(ray_, out hit_, Mathf.Infinity))
        {

            if (lastSelectedPiece != hit_.transform.gameObject)
            {

                hit_.transform.gameObject.GetComponent<GameBoardPiece>().setActive(true);

                if (lastSelectedPiece != null)
                {
                    lastSelectedPiece.GetComponent<GameBoardPiece>().setActive(false);
                }

                lastSelectedPiece = hit_.transform.gameObject;

            }

            if (isPlantingPlant)
            {

                if (getPlantByPosition(lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY) == null)
                {

                    lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.green);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        instantiatePlant(plantToPlantId, lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY);
                        isPlantingPlant = false;
                        plantToPlantId = 0;

                    }


                }
                else
                {

                    lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.red);

                }

            }
            else if (isMovingPlant)
            {

                if (getPlantByPosition(lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY) == null)
                {

                    lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.green);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        selectedPlant.posX = lastSelectedPiece.GetComponent<GameBoardPiece>().posX;
                        selectedPlant.posY = lastSelectedPiece.GetComponent<GameBoardPiece>().posY;

                        float halfHeight_ = ((float)boardHeight / 2) - 0.5f;
                        float halfWidth_ = ((float)boardWidth / 2) - 0.5f;

                        selectedPlant.transform.position = new Vector3(selectedPlant.posX - halfWidth_, 0, selectedPlant.posY - halfHeight_);

                        isMovingPlant = false;
                        selectedPlant = null;

                    }


                }
                else
                {

                    if (getPlantByPosition(lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY) == selectedPlant)
                    {

                        lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.yellow);

                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {

                            isMovingPlant = false;
                            selectedPlant = null;

                        }


                    }

                    lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.yellow);

                }

            }
            else
            {

                if (getPlantByPosition(lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY) != null)
                {

                    lastSelectedPiece.GetComponent<GameBoardPiece>().setColor(Color.green);

                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {

                        isMovingPlant = true;
                        selectedPlant = getPlantByPosition(lastSelectedPiece.GetComponent<GameBoardPiece>().posX, lastSelectedPiece.GetComponent<GameBoardPiece>().posY);

                    }

                }
                else
                {

                    lastSelectedPiece.GetComponent<GameBoardPiece>().alpha = 0;

                }

            }


        }
        else
        {

            if (lastSelectedPiece != null)
            {
                lastSelectedPiece.GetComponent<GameBoardPiece>().setActive(false);

                lastSelectedPiece = null;
            }

        }

    }

    void InstantiateGameBoard()
    {
        foreach (GameObject piece_ in gameboardPieces)
        {

            Destroy(piece_);

        }

        gameboardPieces = new List<GameObject>();

        float halfHeight_ = ((float)boardHeight / 2) - 0.5f;
        float halfWidth_ = ((float)boardWidth / 2) - 0.5f;

        for (int boardx_ = 0; boardx_ < boardWidth; boardx_++)
        {

            for (int boardy_ = 0; boardy_ < boardHeight; boardy_++)
            {

                GameObject inst_ = Instantiate(piecePrefab, new Vector3(boardx_ - halfWidth_, 0, boardy_ - halfHeight_), Quaternion.Euler(0, 0, 0), gameboardPivot.transform);
                GameBoardPiece piece_ = inst_.GetComponent<GameBoardPiece>();

                piece_.posX = boardx_;
                piece_.posY = boardy_;

                if ((boardx_ % 2 == 1 && boardy_ % 2 == 1) || (boardx_ % 2 == 0 && boardy_ % 2 == 0))
                {

                    piece_.floorObject.GetComponent<MeshRenderer>().materials[0].color = new Color(0.3f, 0.9f, 0.3f);

                }
                else
                {

                    piece_.floorObject.GetComponent<MeshRenderer>().materials[0].color = new Color(0.1f, 0.6f, 0.1f);
                    
                }


                gameboardPieces.Add(inst_);

            }

        }

    }

    public PlantPiece getPlantByPosition(int posX_, int posY_)
    {

        PlantPiece plant_ = null;

        foreach (PlantPiece p_ in plants)
        {

            if (p_.posX == posX_ && p_.posY == posY_)
            {

                plant_ = p_;
                break;

            }

        }

        return plant_;

    }

    public bool hasPlantInPosition(int posX_, int posY_)
    {

        bool has_ = false;

        foreach (PlantPiece p_ in plants)
        {

            if (p_.posX == posX_ && p_.posY == posY_)
            {

                has_ = true;
                break;

            }

        }

        return has_;

    }

    public void instantiatePlant(int plantId_, int posX_, int posY_)
    {

        if (posX_ < boardWidth && posY_ < boardHeight)
        {

            float halfHeight_ = ((float)boardHeight / 2) - 0.5f;
            float halfWidth_ = ((float)boardWidth / 2) - 0.5f;

            GameObject inst_ = Instantiate(getPlantById(plantId_).prefab, new Vector3(posX_ - halfWidth_, 0, posY_ - halfHeight_), Quaternion.Euler(0, 0, 0), gameboardPivot.transform.parent);
            PlantPiece plantPiece_ = inst_.GetComponent<PlantPiece>();

            plantPiece_.posX = posX_;
            plantPiece_.posY = posY_;

            plants.Add(inst_.GetComponent<PlantPiece>());


        }
        else
        {

            Debug.Log("- plant position is out of board range!");

        }


    }

    public Plant getPlantById(int id_)
    {

        Plant plant_ = null;

        foreach (Plant p_ in plantsList)
        {

            if (p_.id == id_)
            {

                plant_ = p_;

            }
        }

        return plant_;

    }

}
