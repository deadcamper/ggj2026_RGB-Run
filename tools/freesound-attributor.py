import os
import sys
import glob

def parse_filename(path: str) -> str:
    basename = os.path.basename(path)
    components = basename.split("__")
    # print(basename)
    # print(components)
    sound_id = components[0]
    username = components[1]
    filename_short = components[2]
    url = f"https://freesound.org/people/{username}/sounds/{sound_id}/"
    attribution_str = f"{filename_short} by {username} from {url}"
    return attribution_str

wav_files_relative = glob.glob('../RGB_Run_SRP3D/Assets/Audio/SFX/**/*.wav', recursive=True)
wav_file_attributions = [parse_filename(p) for p in wav_files_relative]
# print("wav_file_attributions: " + str(len(wav_file_attributions)))
print("This game uses the following sounds from freesound.org under Creative Commons licenses, see links for license details.")
for name in wav_file_attributions:
    print(name)

