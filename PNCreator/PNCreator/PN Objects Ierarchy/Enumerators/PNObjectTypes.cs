namespace PNCreator.PNObjectsIerarchy
{
    /// <summary>
    /// Represent a list of all kind of using objects at this application
    /// </summary>
    public enum PNObjectTypes
    {
        DiscreteLocation,
        DiscreteTransition,
        ContinuousLocation,
        ContinuousTransition,
        Membrane,
        StructuralMembrane,
        DiscreteArc,
        DiscreteInhibitorArc,
        DiscreteTestArc,
        ContinuousArc,
        ContinuousInhibitorArc,
        ContinuousTestArc,
        ContinuousFlowArc,
        None
    }
}
