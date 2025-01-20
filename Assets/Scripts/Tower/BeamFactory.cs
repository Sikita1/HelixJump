using UnityEngine;

class BeamFactory
{
    public Beam Create(Beam tower, Transform position, float scaleY)
    {
        Beam beam = GameObject.Instantiate(tower, position);
        beam.transform.localScale = new Vector3(1, scaleY, 1);

        return beam;
    }
}

