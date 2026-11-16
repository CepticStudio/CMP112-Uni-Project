# Cold Nine - Morgue FREE

Six catalogue assets: dry tiled floor (source ID 49), tiled wall (54), surgical armchair (5), surgical forceps (24 / tool_01), scalpel (26 / tool_03) and bone saw (27 / tool_04).

The wall and floor repeat to form a small morgue corner. The instruments are three of the nine entries in Surgical Tools & Medical Props. No character, cold-storage set, organs, jars, rubbish, signs or full environment level is included. Shared texture atlases support these meshes; atlas artwork does not include additional models.

Static props only. No surgical gameplay, animation or rigging. LOD0 only. No audio. Lights are scene objects, never part of asset prefabs or Blueprints. The overview enlarges the instruments for visibility; reusable assets retain their source scale.

Source asset paths are retained. Back up your project before importing overlapping editions. This edition fixes FBX orientation and uses the canonical shared RSG_Lit shader GUID; the September Complete Unity package uses different orientation data and a different shader GUID. Keep that Complete release in a separate project until its shared-asset compatibility has been updated.

Support: RetroStyleGames - https://retrostylegames.com
Use under the license accompanying your acquisition. Free distribution does not place the assets in the public domain.

## Unity setup

Unity 6000.4.10f1. Set color space to Linear. Built-in requires Shader Graph 17.4.0; URP requires Universal RP 17.4.0; HDRP requires High Definition RP 17.4.0. SRP packages include Shader Graph. No Post Processing package is required by these scenes.

Import Morgue_Free.unitypackage. Open a matching scene under Assets/RetroStyleGames/MorgueHorrorPack/Free/Scenes: Demo_Morgue_Free_BRP / URP / HDRP or Overview_Morgue_Free_BRP / URP / HDRP.
For URP/HDRP assign the included MorgueURP / MorgueHDRP render pipeline asset from that pipeline's Settings folder to Graphics settings and the active Quality level. For Built-in leave both unset. Installing a render pipeline package does not activate it.

## Contents and materials

6 FBX meshes; 24 prefabs (6 base prefabs plus 6 per pipeline); 12 materials; 13 PNG textures; 2 shared RSG Shader Graphs; 6 scenes.
Unity imports 7,889 vertices and 11,098 triangles across the six models. Counts are for imported runtime meshes, not editable polygon counts.
Textures: four 1024x1024 floor maps, six 2048x2048 maps and three 4096x4096 armchair maps. Base colour is sRGB; mask, coat and normal maps are linear. Normals are OpenGL +Y. Packed mask: metallic R, AO G, smoothness A. The floor uses RSG_Lit_Coat.

Prefab roots are identity transforms. Floor collision is a 2 cm thick BoxCollider; other meshes use static non-convex MeshColliders. For dynamic Rigidbody gameplay supply suitable convex or compound collision. The floor is a single-sided surface; place it top side up.

Scenes contain a camera and two directional lights. HDRP uses fixed exposure. No baked lightmaps or gameplay controller are included. Rendering differs between pipelines.

AI-generated content: Yes.
