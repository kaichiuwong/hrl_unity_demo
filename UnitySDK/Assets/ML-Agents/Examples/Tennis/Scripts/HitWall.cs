using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWall : MonoBehaviour
{
    public GameObject areaObject;
    public int lastAgentHit;

    private TennisArea area;
    private TennisAgent agentA1;
    private TennisAgent agentB1;
    private TennisAgent agentA2;
    private TennisAgent agentB2;

    void debugAward(string situation)
    {
        /*
           Debug.Log(string.Format("Event: {4} - Agent A1: {0};" +
                                   "Agent A2: {1};" +
                                   "Agent B1: {2};" +
                                   "Agent B2: {3};" +
                                   "lastAgentHit: {5}",
                                   agentA1.GetReward(), agentA2.GetReward(), agentB1.GetReward(), agentB2.GetReward(), situation, lastAgentHit));
        */
    }

    void debugScore()
    {
        Debug.Log(string.Format("{0} - {3} - Side A (HCAPPO) {1} - Side B (PPO) - {2}",
                    System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    agentA1.score,
                    agentB1.score,
                    area.name));
    }

    // Use this for initialization
    void Start()
    {
        area = areaObject.GetComponent<TennisArea>();
        agentA1 = area.agentA1.GetComponent<TennisAgent>();
        agentB1 = area.agentB1.GetComponent<TennisAgent>();
        agentA2 = area.agentA2.GetComponent<TennisAgent>();
        agentB2 = area.agentB2.GetComponent<TennisAgent>();

        agentA1.setTeammate(agentA2);
        agentA2.setTeammate(agentA1);
        agentB1.setTeammate(agentB2);
        agentB2.setTeammate(agentB1);
    }

    private void OnTriggerExit(Collider other)
    {
        debugAward("Start - Over the net");
        if (other.name == "over")
        {
            switch (lastAgentHit)
            {
                case 0: agentA1.AddReward(0.1f); agentA2.AddReward(0.1f); break;
                case 1: agentA1.AddReward(0.1f); agentA2.AddReward(0.1f); break;
                case 2: agentB1.AddReward(0.1f); agentB2.AddReward(0.1f); break;
                case 3: agentB1.AddReward(0.1f); agentB2.AddReward(0.1f); break;
            }
        }
        debugAward("End - Over the net");
    }

    private void OnCollisionEnter(Collision collision)
    {
        debugAward("Start - Collision");
        if (collision.gameObject.CompareTag("iWall"))
        {
            if (collision.gameObject.name == "wallA")
            {
                agentA1.AddReward(-0.01f);
                agentA2.AddReward(-0.01f);
                agentB1.SetReward(0);
                agentB2.SetReward(0);
                agentB1.score += 1;
                agentB2.score = agentB1.score;
                debugAward("Hit Wall A");
            }
            else if (collision.gameObject.name == "wallB")
            {
                agentA1.SetReward(0);
                agentA2.SetReward(0);
                agentB1.AddReward(-0.01f);
                agentB2.AddReward(-0.01f);
                agentA1.score += 1;
                agentA2.score = agentA1.score;
                debugAward("Hit Wall B");
            }
            else if (collision.gameObject.name == "floorA")
            {
                agentA1.AddReward(-0.01f);
                agentA2.AddReward(-0.01f);
                agentB1.SetReward(0);
                agentB2.SetReward(0);
                agentB1.score += 1;
                agentB2.score = agentB1.score;
                debugAward("Hit Floor A");
            }
            else if (collision.gameObject.name == "floorB")
            {
                agentA1.SetReward(0);
                agentA2.SetReward(0);
                agentB1.AddReward(-0.01f);
                agentB2.AddReward(-0.01f);
                agentA1.score += 1;
                agentA2.score = agentA1.score;
                debugAward("Hit Floor B");
            }
            else if (collision.gameObject.name == "net")
            {
                switch (lastAgentHit)
                {
                    case 0:
                        agentA1.AddReward(-0.01f);
                        agentA2.AddReward(-0.01f);
                        agentB1.SetReward(0);
                        agentB2.SetReward(0);
                        agentB1.score += 1;
                        agentB2.score = agentB1.score;
                        break;
                    case 1:
                        agentA1.AddReward(-0.01f);
                        agentA2.AddReward(-0.01f);
                        agentB1.SetReward(0);
                        agentB2.SetReward(0);
                        agentB1.score += 1;
                        agentB2.score = agentB1.score;
                        break;
                    case 2:
                        agentA1.SetReward(0);
                        agentA2.SetReward(0);
                        agentB1.AddReward(-0.01f);
                        agentB2.AddReward(-0.01f);
                        agentA1.score += 1;
                        agentA2.score = agentA1.score;
                        break;
                    case 3:
                        agentA1.SetReward(0);
                        agentA2.SetReward(0);
                        agentB1.AddReward(-0.01f);
                        agentB2.AddReward(-0.01f);
                        agentA1.score += 1;
                        agentA2.score = agentA1.score;
                        break;
                    default: break;
                }
                debugAward("Hit Net");
            }
            debugScore();
            agentA1.Done();
            agentA2.Done();
            agentB1.Done();
            agentB2.Done();
            area.MatchReset();
        }

        if (collision.gameObject.CompareTag("agent"))
        {
            //lastAgentHit = collision.gameObject.name == "AgentA" ? 0 : 1;
            switch (collision.gameObject.name)
            {
                case "AgentA1": lastAgentHit = 0; break;
                case "AgentA2": lastAgentHit = 1; break;
                case "AgentB1": lastAgentHit = 2; break;
                case "AgentB2": lastAgentHit = 3; break;
            }
            debugAward("Hit Agent");
        }
        debugAward("End - Collision");
    }
}