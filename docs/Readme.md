# Demonstration of Hierarchical and Non-Hierarchical Multi-Agent Interactions Based on Unity Reinforcement Learning

## Installation & Set-up

**Environment Requirements**
It is recommended to use Anaconda to manage to python packages. For details on installing Anaconda, please go to [Anaconda Installation Guide](https://docs.anaconda.com/anaconda/install/)

**Unity Environment**
This repository is tested under Unity version 2018.4.8f1 LTS. You could obtain a license from [Unity Download](https://unity3d.com/get-unity/download) to get a copy of Unity.

**Python Packages**
- Python 3.6
- mlagents 0.5.0
- tensorflow 1.7.1

**Settings of the Crawler Scenario** 
|Setting|Description|
| --- | --- |
|Goal|Agent must learn to maintain their body balance and not touch the ground and to fight against the opponent to make the challenger lose their balance.|
|Reward|+1 if opponent’s body touches the ground. <br /> -1 if agent’s body touches the ground.<br />+0.03 times body velocity towards opponent’s direction.<br />+0.01 times body direction alignment with opponent’s direction.|
|Action Space|Rotations of joints (20 variables).|
|Observation Space|Position, rotation, velocity, and angular velocity of each limb, plus the acceleration and angular acceleration of the body (117 variables).|
|Manager Observation|Position, rotation, velocity, distance, and angular velocity of each limb, plus the acceleration and angular acceleration of the body (119 variables)|

**Settings of the Tennis Scenario** 
|Setting|Description|
| --- | --- |
|Goal|Agents shall not miss the ball or let the ball fall out of the court area during the episode by striking the ball over the net into the opponents’ court.|
|Reward|+0.1 when the ball is hit over the net<br />-0.1 when the agent misses the ball or the ball falls out of the tennis court|
|Action Space|The movement forward or away from the net, as well as jumping (3 variables).|
|Observation Space|Position and velocity information of the ball, racket and teammate (10 variables).|
|Manager Observation|Position, velocity and distance information of the ball, racket and teammate (14 variables)|

**Settings of the Banana Collector Scenario** 
|Setting|Description|
| --- | --- |
|Goal|Agents must learn to collect as many healthy bananas as possible while avoiding toxic bananas.|
|Reward|+1 when an agent collects a yellow healthy banana<br />-1 when an agent collects a purple toxic banana|
|Action Space|4 Branches of Action<br />Movement Branch: Forward, Backward or No Action<br />Side Motion Branch: Left, Right or No Action Rotation Branch: Rotate Left, Rotate Right or No Action<br />Laser Branch: Emit a laser or No Action|
|Observation Space|Velocity of agents and the ray-based angle information of the objects in front of the agents (7 raycast angles with 7 measurements for each an- gle, 53 variables in total)|
|Manager Observation|Velocity and distance of agents, with the ray-based angle information of the objects in front of the agents (7 raycast angles with 8 measurements for each angle, 60 variables in total)|

**Settings of the Soccer Scenario** 
|Setting|Description|
| --- | --- |
|Goal|**Striker**: agents need to calculate a method to kick the ball into the opponent’s goal.<br />**Goalie**: agents need to learn to defend against the opponent and to avoid the ball being kicked into their own goal.|
|Reward|**Striker**: +1 when the ball enters the opponent’s goal, -0.1 when the ball enters their own goal.<br /> **Goalie**: -1 when the ball enters their own team’s goal, +0.1 when the ball enters the oppo- nents goal.|
|Action Space|**Striker**: Forward, backward, rotation and side- ways movement (6 variables)<br /> **Goalie**: Forward, backward and sideways movement (4 variables)|
|Observation Space|Seven types of object detection, with distance information in 180 degree of view (112 variables)|
|Manager Observation|Eight types of object detection, with distance information in 270 degree of view (200 variables)|


## Test the Demonstration Scenarios
1. Open the Scenario in Unity
2. Assume the Tennis scenario is opened. In the Project window, go to Assets/ML-Agents/Examples/Tennis/Brains folder and drag the Tennis Brain to the Brains property under Braodcast Hub in the TennisAcademy object in the Inspector window. It is to make sure the trained Brain is assigned to the agent.
3. Click the Play button in Unity to start the test.

**Note**: Assigning a Brain to an agent (dragging a Brain into the Brain property of the agent) means that the Brain will be making decision for that agent. Whereas dragging a Brain into the Broadcast Hub means that the Brain will be exposed to the Python process. The Control checkbox means that in addition to being exposed to Python, the Brain will be controlled by the Python process (*required for training*).

## Training a Scenario
1. You can train a scenario by using `mlagents-learn` command from Unity ML Agent toolkits
2. Open terminal 
3. Change directory to the folder of ML-Agents Toolkit. It is usually under `ml-agents/` folder.
4. Run `mlagents-learn <trainer-config-file> --env=<env_name> --run-id=<run-identifier>`
   - `<trainer-config-file>` a YAML file that storing the scenario configuration
   - `env_name` the name of the scenario, usually is the executable name from Unity export
   - `<run-identifier>` a string to identify your run
   E.g.
   ```
   mlagents-learn config/ppo/Tennis.yaml --env=Tennis --run-id=tennis_run
   ```
5. After the command is triggered, you could see the following screen. It indicates that the training is started.
   ```


                        ▄▄▄▓▓▓▓
                   ╓▓▓▓▓▓▓█▓▓▓▓▓
              ,▄▄▄m▀▀▀'  ,▓▓▓▀▓▓▄                           ▓▓▓  ▓▓▌
            ▄▓▓▓▀'      ▄▓▓▀  ▓▓▓      ▄▄     ▄▄ ,▄▄ ▄▄▄▄   ,▄▄ ▄▓▓▌▄ ▄▄▄    ,▄▄
          ▄▓▓▓▀        ▄▓▓▀   ▐▓▓▌     ▓▓▌   ▐▓▓ ▐▓▓▓▀▀▀▓▓▌ ▓▓▓ ▀▓▓▌▀ ^▓▓▌  ╒▓▓▌
        ▄▓▓▓▓▓▄▄▄▄▄▄▄▄▓▓▓      ▓▀      ▓▓▌   ▐▓▓ ▐▓▓    ▓▓▓ ▓▓▓  ▓▓▌   ▐▓▓▄ ▓▓▌
        ▀▓▓▓▓▀▀▀▀▀▀▀▀▀▀▓▓▄     ▓▓      ▓▓▌   ▐▓▓ ▐▓▓    ▓▓▓ ▓▓▓  ▓▓▌    ▐▓▓▐▓▓
          ^█▓▓▓        ▀▓▓▄   ▐▓▓▌     ▓▓▓▓▄▓▓▓▓ ▐▓▓    ▓▓▓ ▓▓▓  ▓▓▓▄    ▓▓▓▓`
            '▀▓▓▓▄      ^▓▓▓  ▓▓▓       └▀▀▀▀ ▀▀ ^▀▀    `▀▀ `▀▀   '▀▀    ▐▓▓▌
               ▀▀▀▀▓▄▄▄   ▓▓▓▓▓▓,                                      ▓▓▓▓▀
                   `▀█▓▓▓▓▓▓▓▓▓▌
                        ¬`▀▀▀█▓
   ```

## Switch to Hierarchical Trainer
1. Modify the number of observation of Brain in Unity with the Manager Observation
2. Export a new executable
3. Rename 'ppo.hca' folder to 'ppo' folder.
4. Rename one of 'trainer_HRL.py' to 'trainer.py' in ppo folder. 
5. Restart the training as above steps

## Switch back to Original Trainer
1. Modify the number of observation of Brain in Unity back to the original setting (Without Manager Observation)
2. Export a new executable
3. Rename 'ppo.org' folder to 'ppo' folder.
4. Restart the training as above steps
