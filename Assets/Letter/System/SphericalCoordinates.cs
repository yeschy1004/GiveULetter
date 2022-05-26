using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalCoordinates
{
    public float radius;
    public float polar;
    public float azimuthal;

    public SphericalCoordinates() {
        radius = 0;
        polar = 0;
        azimuthal = 0;
    }

    public SphericalCoordinates(float rho, float theta, float phi)
    {
        radius = rho;
        polar = theta;
        azimuthal = phi;
    }

    public static Vector3 Spherical2Cartesian(SphericalCoordinates coordinates)
    {
        Vector3 cartesian = new Vector3();
        cartesian.x = coordinates.radius * Mathf.Sin(coordinates.azimuthal) * Mathf.Cos(coordinates.polar);
        cartesian.z = coordinates.radius * Mathf.Sin(coordinates.azimuthal) * Mathf.Sin(coordinates.polar);
        cartesian.y = coordinates.radius * Mathf.Cos(coordinates.azimuthal);
        return cartesian;
    }

    //takes radian.
    public static SphericalCoordinates Cartesian2Spherical(Vector3 coordinates)
    {
        SphericalCoordinates spherical = new SphericalCoordinates();
        spherical.radius = Mathf.Sqrt(Mathf.Pow(coordinates.x, 2) + Mathf.Pow(coordinates.y, 2) + Mathf.Pow(coordinates.z, 2));
        spherical.polar = Mathf.Atan2(coordinates.z, coordinates.x);
        spherical.azimuthal = Mathf.Atan2(Mathf.Sqrt(Mathf.Pow(coordinates.x, 2) + Mathf.Pow(coordinates.z, 2)), coordinates.y);
        return spherical;
    }

    public static string PrintSpherical(SphericalCoordinates coordinates)
    {
        return "(" + coordinates.radius + ", " + coordinates.polar + ", " + coordinates.azimuthal + ")";
    }
}
