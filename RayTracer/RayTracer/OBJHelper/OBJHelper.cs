using System;
using System.IO;
using System.Numerics;
using System.Runtime.Intrinsics;

namespace RayTracer.RayTracer;

public class OBJHelper
{
    public static Vertex[] Read(string filePath)
    {
        //Vertex portions
        List<Vector3F> vertex_positions = new();
        List<Vector3F> vertex_texcoords = new();
        List<Vector3F> vertex_normals = new();

        //Face vectors
        List<int> vertex_positions_incdicies = new();
        List<int> vertex_texcoords_incdicies = new();
        List<int> vertex_normals_incdicies = new();

        foreach (var line in File.ReadLines(filePath))
        {
            Vector3F temp_vec3 = new();

            var lineContent = line.Split(' ');
            string prefix = lineContent[0];

            switch (prefix)
            {
                case "#":
                    break;
                case "o":
                    break;
                case "s":
                    break;
                case "usemtl":
                    break;
                case "v": //Vertex Position
                    temp_vec3.X = float.Parse(lineContent[1]);
                    temp_vec3.Y = float.Parse(lineContent[2]);
                    temp_vec3.Z = float.Parse(lineContent[3]);
                    vertex_positions.Add(temp_vec3);
                    break;
                case "vt":
                    temp_vec3.X = float.Parse(lineContent[1]);
                    temp_vec3.Y = float.Parse(lineContent[2]);
                    temp_vec3.Z = 0;
                    vertex_texcoords.Add(temp_vec3);
                    break;
                case "vn":
                    temp_vec3.X = float.Parse(lineContent[1]);
                    temp_vec3.Y = float.Parse(lineContent[2]);
                    temp_vec3.Z = float.Parse(lineContent[3]);
                    vertex_normals.Add(temp_vec3);
                    break;
                case "f":
                    for (int i = 1; i < lineContent.Length; i++)
                    {
                        var vec3Data = lineContent[i].Split('/');
                        for (int j = 0; j < vec3Data.Length; j++)
                        {
                            int index = int.Parse(vec3Data[j]);
                            switch (j)
                            {
                                case 0:
                                    vertex_positions_incdicies.Add(index);
                                    break;
                                case 1:
                                    vertex_texcoords_incdicies.Add(index);
                                    break;
                                case 2:
                                    vertex_normals_incdicies.Add(index);
                                    break;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        Vertex[] verticies = new Vertex[vertex_positions_incdicies.Count];

        for (int i = 0; i < verticies.Length; i++)
        {
            verticies[i] = new Vertex()
            {
                Position = vertex_positions[vertex_positions_incdicies[i] - 1],
                TexCoord = vertex_texcoords[vertex_texcoords_incdicies[i] - 1],
                Normal = vertex_normals[vertex_normals_incdicies[i] - 1]
            };
        }

        Console.WriteLine($"Nr of vertices: {verticies.Length}");

        Console.WriteLine($"vertex_positions_incdicies: {vertex_positions_incdicies.Count}");
        Console.WriteLine($"vertex_texcoords_incdicies: {vertex_texcoords_incdicies.Count}");
        Console.WriteLine($"vertex_normals_incdicies: {vertex_normals_incdicies.Count}");

        Console.WriteLine($"vertex_positions: {vertex_positions.Count}");
        Console.WriteLine($"vertex_texcoords: {vertex_texcoords.Count}");
        Console.WriteLine($"vertex_normals: {vertex_normals.Count}");

        Console.WriteLine("OBJ file is loaded!");

        return verticies;
    }
}
