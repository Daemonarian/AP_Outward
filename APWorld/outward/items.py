from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Item, ItemClassification

from .common import OUTWARD
from .templates import OutwardGameObjectNamespace, OutwardGameObjectTemplate

if TYPE_CHECKING:
    from . import OutwardWorld

# classes for generating items in the multiworld

class OutwardItem(Item):
    game = OUTWARD

class OutwardGameItem(OutwardItem):
    def __init__(self, world: OutwardWorld, name: str):
        template = OutwardGameItemTemplate.get_template(name)
        super().__init__(template.name, template.classification, template.archipelago_id, world.player)
        world.multiworld.itempool.append(self)

# classes for defining the basic properties of game items

class OutwardGameItemTemplate(OutwardGameObjectTemplate):
    _classification: ItemClassification

    def __init__(self, name: str, classification: ItemClassification, archipelago_id: int = -1):
        super().__init__(name, archipelago_id)
        self._classification = classification

    @property
    def classification(self) -> ItemClassification:
        return self._classification

item = OutwardGameItemTemplate.register_template
class OutwardItemName(OutwardGameObjectNamespace):
    template = OutwardGameItemTemplate

    # key items

    QUEST_LICENSE = item("Progressive Quest License", ItemClassification.progression)

    # useful gear
    
    CEREMONIAL_BOW = item("Ceremonial Bow", ItemClassification.progression)
    COMPASSWOOD_STAFF = item("Compasswood Staff", ItemClassification.progression)
    CRACKED_RED_MOON = item("Cracked Red Moon", ItemClassification.progression)
    DEPOWERED_BLUDGEON = item("De-powered Bludgeon", ItemClassification.progression)
    EXPERIMENTAL_CHAKRAM = item("Experimental Chakram", ItemClassification.progression)
    FOSSILIZED_GREATAXE = item("Fossilized Greataxe", ItemClassification.progression)
    KRYPTEIA_TOMB_KEY = item("Krypteia Tomb Key", ItemClassification.progression)
    MYRMITAUR_HAVEN_GATE_KEY = item("Myrmitaur Haven Gate Key", ItemClassification.progression)
    MYSTERIOUS_LONG_BLADE = item("Mysterious Long Blade", ItemClassification.progression)
    RED_LADYS_DAGGER = item("Red Lady's Dagger", ItemClassification.progression)
    RUINED_HALBERD = item("Ruined Halberd", ItemClassification.progression)
    RUSTED_SPEAR = item("Rusted Spear", ItemClassification.progression)
    SCARLET_DAGGER = item("Scarlet Dagger", ItemClassification.progression)
    SCARLET_GEM = item("Scarlet Gem", ItemClassification.progression)
    SCARLET_LICHS_IDOL = item("Scarlet Lich's Idol", ItemClassification.progression)
    SCARRED_DAGGER = item("Scarred Dagger", ItemClassification.progression)
    SEALED_MACE = item("Sealed Mace", ItemClassification.progression)
    SLUMBERING_SHIELD = item("Slumbering Shield", ItemClassification.progression)
    SMELLY_SEALED_BOX = item("Smelly Sealed Box", ItemClassification.progression)
    STRANGE_RUSTED_SWORD = item("Strange Rusted Sword", ItemClassification.progression)
    UNUSUAL_KNUCKLES = item("Unusual Knuckles", ItemClassification.progression)
    WARM_AXE = item("Warm Axe", ItemClassification.progression)

    ANGLER_SHIELD = item("Angler Shield", ItemClassification.useful)
    ANTIQUE_PLATE_BOOTS = item("Antique Plate Boots", ItemClassification.useful)
    ANTIQUE_PLATE_GARB = item("Antique Plate Garb", ItemClassification.useful)
    ANTIQUE_PLATE_SALLET = item("Antique Plate Sallet", ItemClassification.useful)
    BLUE_SAND_ARMOR = item("Blue Sand Armor", ItemClassification.useful)
    BLUE_SAND_BOOTS = item("Blue Sand Boots", ItemClassification.useful)
    BLUE_SAND_HELM = item("Blue Sand Helm", ItemClassification.useful)
    BRAND = item("Brand", ItemClassification.useful)
    BRASS_WOLF_BACKPACK = item("Brass-Wolf Backpack", ItemClassification.useful)
    COPAL_ARMOR = item("Copal Armor", ItemClassification.useful)
    COPAL_BOOTS = item("Copal Boots", ItemClassification.useful)
    COPAL_HELM = item("Copal Helm", ItemClassification.useful)
    DISTORTED_EXPERIMENT = item("Distorted Experiment", ItemClassification.useful)
    DREAMER_HALBERD = item("Dreamer Halberd", ItemClassification.useful)
    DUTY = item("Duty", ItemClassification.useful)
    FABULOUS_PALLADIUM_SHIELD = item("Fabulous Palladium Shield", ItemClassification.useful)
    GEPS_LONGBLADE = item("Gep's Longblade", ItemClassification.useful)
    GHOST_PARALLEL = item("Ghost Parallel", ItemClassification.useful)
    GILDED_SHIVER_OF_TRAMONTANE = item("Gilded Shiver of Tramontane", ItemClassification.useful)
    GOLD_LICH_ARMOR = item("Gold-Lich Armor", ItemClassification.useful)
    GOLD_LICH_BOOTS = item("Gold-Lich Boots", ItemClassification.useful)
    GOLD_LICH_MASK = item("Gold-Lich Mask", ItemClassification.useful)
    GOLD_LICH_SPEAR = item("Gold-Lich Spear", ItemClassification.useful)
    GRIND = item("Grind", ItemClassification.useful)
    JADE_LICH_BOOTS = item("Jade-Lich Boots", ItemClassification.useful)
    JADE_LICH_MASK = item("Jade-Lich Mask", ItemClassification.useful)
    JADE_LICH_ROBES = item("Jade-Lich Robes", ItemClassification.useful)
    JADE_LICH_STAFF = item("Jade-Lich Staff", ItemClassification.useful)
    LIGHT_MENDERS_BACKPACK = item("Light Mender's Backpack", ItemClassification.useful)
    LIGHT_MENDERS_LEXICON = item("Light Mender's Lexicon", ItemClassification.useful)
    MEFINOS_TRADE_BACKPACK = item("Mefino's Trade Backpack", ItemClassification.useful)
    MERTONS_FIREPOKER = item("Merton's Firepoker", ItemClassification.useful)
    MERTONS_RIBCAGE = item("Merton's Ribcage", ItemClassification.useful)
    MERTONS_SHINBONES = item("Merton's Shinbones", ItemClassification.useful)
    MERTONS_SKULL = item("Merton's Skull", ItemClassification.useful)
    MURMURE = item("Murmure", ItemClassification.useful)
    MYSTERIOUS_CHAKRAM = item("Mysterious Chakram", ItemClassification.useful)
    ORNATE_BONE_SHIELD = item("Ornate Bone Shield", ItemClassification.useful)
    PALLADIUM_ARMOR = item("Palladium Armor", ItemClassification.useful)
    PALLADIUM_BOOTS = item("Palladium Boots", ItemClassification.useful)
    PALLADIUM_HELM = item("Palladium Helm", ItemClassification.useful)
    PEARLESCENT_MAIL = item("Pearlescent Mail", ItemClassification.useful)
    PETRIFIED_WOOD_ARMOR = item("Petrified Wood Armor", ItemClassification.useful)
    PETRIFIED_WOOD_BOOTS = item("Petrified Wood Boots", ItemClassification.useful)
    PETRIFIED_WOOD_HELM = item("Petrified Wood Helm", ItemClassification.useful)
    PILLAR_GREATHAMMER = item("Pillar Greathammer", ItemClassification.useful)
    PORCELAIN_FISTS = item("Porcelain Fists", ItemClassification.useful)
    REVENANT_MOON = item("Revenant Moon", ItemClassification.useful)
    ROTWOOD_STAFF = item("Rotwood Staff", ItemClassification.useful)
    RUST_LICH_ARMOR = item("Rust Lich Armor", ItemClassification.useful)
    RUST_LICH_BOOTS = item("Rust Lich Boots", ItemClassification.useful)
    RUST_LICH_HELMET = item("Rust Lich Helmet", ItemClassification.useful)
    SANDROSE = item("Sandrose", ItemClassification.useful)
    SCARLET_BOOTS = item("Scarlet Boots", ItemClassification.useful)
    SCARLET_MASK = item("Scalet Mask", ItemClassification.useful)
    SCARLET_ROBES = item("Scarlet Robes", ItemClassification.useful)
    SCEPTER_OF_THE_CRUEL_PRIEST = item("Scepter of the Cruel Priest", ItemClassification.useful)
    SHRIEK = item("Shriek", ItemClassification.useful)
    SKYCROWN_MACE = item("Skycrown Mace", ItemClassification.useful)
    STARCHILD_CLAYMORE = item("Starchild Claymore", ItemClassification.useful)
    SUNFALL_AXE = item("Sunfall Axe", ItemClassification.useful)
    TENEBROUS_ARMOR = item("Tenebrous Armor", ItemClassification.useful)
    TENEBROUS_BOOTS = item("Tenebrous Boots", ItemClassification.useful)
    TENEBROUS_HELM = item("Tenebrous Helm", ItemClassification.useful)
    THRICE_WROUGHT_HALBERD = item("Thrice-Wrought Halberd", ItemClassification.useful)
    TOKEBAKICIT = item("Tokebakicit", ItemClassification.useful)
    TSAR_ARMOR = item("Tsar Armor", ItemClassification.useful)
    TSAR_BOOTS = item("Tsar Boots", ItemClassification.useful)
    TSAR_FISTS = item("Tsar Fists", ItemClassification.useful)
    TSAR_HELM = item("Tsar Helm", ItemClassification.useful)
    WERLIG_SPEAR = item("Werlig Spear", ItemClassification.useful)
    WILL_O_WISP = item("The Will O Wisp", ItemClassification.useful)
    WORLDEDGE_GREATAXE = item("Worldedge Greataxe", ItemClassification.useful)
    ZHORNS_DEMON_SHIELD = item("Zhorn's Demon Shield", ItemClassification.useful)
    ZHORNS_GLOWSTONE_DAGGER = item("Zhorn's Glowstone Dagger", ItemClassification.useful)
    ZHORNS_HUNTING_BACKPACK = item("Zhorn's Hunting Backpack", ItemClassification.useful)

    # useful skills

    CURSE_HEX = item("Curse Hex", ItemClassification.useful)
    POSSESSED_SKILL = item("Possessed", ItemClassification.useful)

    # filler items

    SILVER_CURRENCY = item("50x Silver", ItemClassification.filler)
