CHERIAT - ANCIENT DUNGEON KIT
Stylized hand-painted modular dungeon assets

Creator: CHERIAT
Edition: Free Sample (2 assets)

This free sample contains 2 assets from the Ancient Dungeon Kit.

INCLUDED ASSETS   (ID, name, triangles, size X x Y x Z in meters, texture size)
  01-02_Wall0               1,048 tris   3.54 x 0.83 x 3.81 m   2048 px
  05-01_Brazier             1,340 tris   2.27 x 2.23 x 1.59 m   2048 px

FORMATS (each format is a separate download)
  FBX package  - one FBX file per asset, PNG textures in the Textures folder
  GLB package  - one GLB file per asset, textures embedded

FOLDER STRUCTURE (folders present depend on the package you downloaded)
  FBX/        FBX models
  GLB/        GLB models
  Textures/   PNG texture maps
  README.txt
  LICENSE.txt

Keep the FBX and Textures folders side by side: the FBX files look for their textures in ../Textures.

TEXTURES
  Each asset has one material and one texture set named <Asset ID>_<Map>.png:
  BaseColor (sRGB) and Roughness (non-color) for every asset.
  Metallic (non-color) for assets with metal or gold parts.
  Emission (sRGB) for assets with crystals, fire, lamps, glass or magic effects.
  Texture size per asset is listed above (2048 or 1024 px).

SCALE
  Units are meters. Each asset is a single mesh with its pivot at the bottom center.
  The wall, arch, door and gate pieces (Sets 01 and 04) were scaled together for modular building.

IMPORT - BLENDER
  FBX: File > Import > FBX (.fbx). Textures load automatically when the Textures folder sits next to the FBX folder.
  GLB: File > Import > glTF 2.0 (.glb/.gltf). Textures are embedded.

IMPORT - ROBLOX STUDIO
  1. Open the 3D Importer (File > Import 3D) and select an FBX file.
  2. If the textures are not applied automatically, add a SurfaceAppearance to the MeshPart and assign
     BaseColor to ColorMap, Roughness to RoughnessMap and Metallic to MetalnessMap.
  3. Emission maps are not used by SurfaceAppearance; add lights or glow in Studio where needed.
  Check each mesh against the current Roblox triangle and texture limits before publishing.
  Import results can vary between Studio versions.

OTHER ENGINES
  FBX and GLB are standard exchange formats. Import into other engines has not been tested; material setup may need adjusting.

GLB NOTES
  GLB files follow the glTF 2.0 standard: roughness and metallic are stored together in one embedded texture.
  Emissive strength uses the KHR_materials_emissive_strength extension, which some viewers ignore.

RECOMMENDED USE
  Dungeon levels, ancient ruins, fantasy RPG and adventure environments, Roblox experiences and prototypes.

USAGE
  You may use and modify these assets in personal and commercial games, including Roblox experiences,
  and distribute finished games that contain them.
  You may not resell, redistribute or repackage the asset files, upload them as free asset packs, share purchased files,
  or claim ownership of the original CHERIAT assets.
  Attribution is not required. See LICENSE.txt for the full terms.
