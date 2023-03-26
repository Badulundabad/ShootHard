using Unity.Entities;

[WorldSystemFilter(WorldSystemFilterFlags.Default | WorldSystemFilterFlags.Editor | WorldSystemFilterFlags.ThinClientSimulation)]
[UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
[UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
public partial class EarlySimulationSystemGroup : ComponentSystemGroup { }
