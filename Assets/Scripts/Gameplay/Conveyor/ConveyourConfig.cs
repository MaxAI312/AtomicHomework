using GameEngine;
using UnityEngine;

namespace Game.Gameplay.Conveyors
{
    [CreateAssetMenu(
        fileName = "ConveyourConfig",
        menuName = "Content/Conveyor/New ConveyourConfig"
    )]
    public sealed class ConveyourConfig : ScriptableObject
    {
        public ResourceType loadType = ResourceType.WOOD;
        public ResourceType unloadType = ResourceType.LUMBER;

        [Space]
        public int loadCapacity = 12; //Вместимость бревен
        public int unloadCapacity = 24; //Вместимость досок
        
        [Space]
        public int ingredientCount = 1; //Сколько берем бревен на входе
        public int resultCount = 2; //Сколько кладем досок на выходе

        [Space]
        public float workTime = 5;
    }
}