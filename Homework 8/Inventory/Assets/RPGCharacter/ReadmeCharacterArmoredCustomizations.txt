/*
README

RPG Character
Lylek Games

CHARACTER ARMORED CUSTOMIZATIONS----------------

This script was designed to allow customizations in the Editor, rather than having to use the demo scene.
To use this script, first drag and drop the RPGCharacter_armored prefab(s) into your scene. Note that this script will only be useful on such character gameObjects that contain
multiple armor meshes, (like the RPGCharacter_armored prefabs).

Go ahead and drag-and-drop the CharacterArmoredCustomizations script onto your RPGCharacter_arrmored gameObject. You may notice that you can't quite do anything yet! First we require the directory path
of the RPGCharacter folder. (Normally it may reside in the Assets/ folder, however if you have moved it, please specify this new path.) Next, defind what race and gender this chracter is, so we can
acquire the proper resources. Then press Update Character!

You should now have many customization options in front of you. The script has acquire all child character meshes, armrors, and hair styles, and has located the appropriate hair and skin materials
acording to the character's specified race and gender. You can press the drop down arrow on Character Resources to view all acquired customizations.

Feel free to drag the slider bars around! The characters will update in real time. When you have got your desired look for you character you may press 'Permanetly Combine Meshes'. This will use the
CombineSkinMeshesTextureAtlas script to combine your character into a single mesh, as well as remove all unused, hidden meshes, and the CharacterArmoredCustomizations script itself.

To save the character, view the CombineSkinMeshesTextureAtlas script and press Save Data. Do not press Combine Meshes or Disable Meshes, they will not work!

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
