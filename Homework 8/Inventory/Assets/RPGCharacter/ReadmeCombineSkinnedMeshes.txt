/*
README

RPG Character
Lylek Games

COMBINED SKINNED MESHES----------------

To use the combined skinned meshes script in your game, please locate the CombineSkinMeshesTextureAtlas script in
the RPGCharacter/Scripts/Character folder.

Go ahead and place your character into your scene. (If you are using the provided RPGCharacter_skin, or RPGCharacter_armored
prefabs, we have already added the combine skinned meshes script for you.) Your character must be in a default T-Pose. Drag
and drop the script onto your character's root gameObject. The script will automatically atempt to acquire your character's
armature and bones. If either of these variables are inaccurate please assign them manually and then press "Recalculate Bones".

Feel free to now set a SaveName for you character, a texture atlas size, and add any Skinned Meshes to the My Skinned Meshes list.
- When saving, we will use the Save Name as a bases for our file names; such as: "[MyCharacter]_diffuse.png" when saving a texture file.
- Texture Atlas Size will determine the final size of the compiled texture atlas. The default, 1024 x 1024, should work well.
- My Skinned Meshes should include the skinned meshes you wish to combine. If you press "Combine Meshes" while this list is left empty,
the script will automatically search for, acquire, and combine all active skinned mesh children.

Save Mesh Data is ideal for creating NPCs which may persist in different areas of your game. This allows you to customize an NPC character
and save it as a prefab to use elswhere, such as in multiple scenes.

As for characters that may swaps armor at runtime, it may not be nessecary to save this sort of data. Instead, simply call the BeginCombineMeshes,
and DissasembleMeshes methods of the CombineSkinnedMehses script during runtime for quick and easy optimization. You can do this by referencing the
CombineSkinMeshesTextureAtlas script on your character:

//	CombineSkinMeshesTextureAtlas myCombineSkinnedMeshesScript;
//	myCombineSkinnedMeshesScript = GetComponent<CombineSkinMeshesTextureAtlas>();

And calling the proper method:

//	myCombineSkinnedMeshesScript.BeginCombineMeshes();

When swaping our armor, you may wish to disable the combined mesh, and then re-combine them:

//	myCombineSkinnedMeshesScript.DissasembleMesh();
//	Remove/Deactivate my old Helmet...
//	Add/Activate my new Helmet...
//	myCombineSkinnedMeshesScript.BeginCombineMeshes();

IMPORTANT----------------
- For proper texture atlasing, all Skinned Meshes must contain a Standard material with a Diffuse(Albedo), Normal map, and Metallic texture. Each
texture must be of identical size, per material. (Different material may use different sized textures). If your are using a Skinned Mesh that does
not require a Normal map or Metallic texutre, please use a texture that will add little or no affect; such as the provided 'Default' textures, located
in RPGCharacter/Textures/DefaultTextures.
- All textures must have Read/Write Enabled, in the Import Settings.

ADDITIONAL NOTES----------------
- To render proper normal maps we use pre-formated textures which are stored in the Resources/DefaultTextures folder.
- After Saving Mesh Data the files may not display immediately in the project window. Simply minimize and restore Unity
to refresh.
- Please make sure that not only are the textures on your materials the same size, but that the MaxSize compression in the 
Import Settings are the same size as well.

We do our best to make our assets as user-friendly as possible! Please, by all means, do not hesitate
to send us an email if you have any questions or comments!

support@lylekgames.com, or visit http://www.lylekgames.com/contacts

**Please leave a rating and review! Even a small review may help immensely with prioritizing updates.**
(Assets with few and infrequent reviews/ratings tend to have less of a priority and my be late or miss-out on crucial compatibility updates, or even be depricated.)
Thank you! =)

*******************************************************************************************

Website
http://www.lylekgames.com/

Support
http://www.lylekgames.com/contacts
*/
