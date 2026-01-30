from __future__ import annotations
from typing import TYPE_CHECKING

from BaseClasses import Location
from worlds.generic.Rules import add_rule

from .common import OUTWARD
from .templates import OutwardGameObjectNamespace, OutwardGameObjectTemplate
from .regions import OutwardRegionName

if TYPE_CHECKING:
    from worlds.generic.Rules import CollectionRule

    from . import OutwardWorld

# classes for generating items in the multi-world

class OutwardLocation(Location):
    game = OUTWARD

    def add_rule(self, rule: CollectionRule, combine: str = "and") -> None:
        add_rule(self, rule, combine)

class OutwardGameLocation(OutwardLocation):
    def __init__(self, world: OutwardWorld, name: str):
        template = OutwardGameLocationTemplate.get_template(name)
        parent = world.get_region(template.region)
        super().__init__(world.player, template.name, template.archipelago_id, parent)
        parent.locations.append(self)
        
# classes for defining the basic properties of game locations

class OutwardGameLocationTemplate(OutwardGameObjectTemplate):
    _region: str
    def __init__(self, name: str, region: str, archipelago_id: int = -1):
        super().__init__(name, archipelago_id)
        self._region = region
    @property
    def region(self) -> str:
        return self._region

# define the names of Outward locations

location = OutwardGameLocationTemplate.register_template
class OutwardLocationName(OutwardGameObjectNamespace):
    template = OutwardGameLocationTemplate

    # quest completion checks

    QUEST_MAIN_01 = location("Main Quest 1 - Castaways", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_02 = location("Main Quest 2 - Call to Adventure", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_03 = location("Main Quest 3 - Join Faction Quest", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_04 = location("Main Quest 4 - Faction Quest 1", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_05 = location("Main Quest 5 - Faction Quest 2", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_06 = location("Main Quest 6 - Faction Quest 3", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_07 = location("Main Quest 7 - Faction Quest 4", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MAIN_08 = location("Main Quest 8 - A Fallen City", OutwardRegionName.CALDERA)
    QUEST_MAIN_09 = location("Main Quest 9 - Up The Ladder", OutwardRegionName.CALDERA)
    QUEST_MAIN_10 = location("Main Quest 10 - Stealing Fire", OutwardRegionName.CALDERA)
    QUEST_MAIN_11 = location("Main Quest 11 - Liberate the Sun", OutwardRegionName.CALDERA)
    QUEST_MAIN_12 = location("Main Quest 12 - Vengeful Ouroboros", OutwardRegionName.CALDERA)
    
    QUEST_PARALLEL_BLOOD_UNDER_THE_SUN = location("Parallel Quest - Blood Under the Sun", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_PARALLEL_PURIFIER = location("Parallel Quest - Purifier", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_1 = location("Parallel Quest - Rust and Vengeance - Rust Lich Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_2 = location("Parallel Quest - Rust and Vengeance - Rust Lich Boots", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_PARALLEL_RUST_AND_VENGEANCE_3 = location("Parallel Quest - Rust and Vengeance - Rust Lich Helmet", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_PARALLEL_VENDAVEL_QUEST = location("Parallel Quest - Vendavel Quest", OutwardRegionName.MAIN_GAME_AREA)

    QUEST_MINOR_ACQUIRE_MANA = location("Minor Quest - Acquire Mana", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_ALCHEMY_COLD_STONE = location("Minor Quest - Alchemy: Cold Stone", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_ALCHEMY_CRYSTAL_POWDER = location("Minor Quest - Alchemy: Crystal Powder", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_ARCANE_MACHINE = location("Minor Quest - Arcane Machine", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BARREL_MAN = location("Minor Quest - Barrel Man", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_1 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Spear", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_2 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Mask", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_3 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Boots", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_4 = location("Minor Quest - Beware the Gold Lich - Gold-Lich Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_1 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Staff", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_2 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Boots", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_3 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Mask", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BEWARE_THE_JADE_LICH_4 = location("Minor Quest - Beware the Jade Lich - Jade-Lich Robes", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_BLOODY_BUSINESS = location("Minor Quest - Bloody Business", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_CRAFT_ANTIQUE_PLATE_GARB_ARMOR = location("Minor Quest - Craft: Antique Plate Garb Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_CRAFT_BLUE_SAND_ARMOR = location("Minor Quest - Craft: Blue Sand Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_CRAFT_COPAL_AND_PETRIFIED_ARMOR = location("Minor Quest - Craft: Copal & Petrified Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_CRAFT_PALLADIUM_ARMOR = location("Minor Quest - Craft: Palladium Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_CRAFT_TSAR_AND_TENEBROUS_ARMOR = location("Minor Quest - Craft: Tsar & Tenebrous Armor", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_HELENS_FUNGUS = location("Minor Quest - Helen's Fungus", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_LEDGER_TO_BERG = location("Minor Quest - Ledger to Berg", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_LEDGER_TO_CIERZO = location("Minor Quest - Ledger to Cierzo", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_LEDGER_TO_LEVANT = location("Minor Quest - Ledger to Levant", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_LEDGER_TO_MONSOON = location("Minor Quest - Ledger to Monsoon", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_LOST_MERCHANT = location("Minor Quest - Lost Merchant", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_MYRIAD_OF_BONES = location("Minor Quest - A Myriad of Bones", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_ANGEL_FOOD_CAKE = location("Minor Quest - Need: Angel Food Cake", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_BEAST_GOLEM_SCRAPS = location("Minor Quest - Need: Beast Golem Scraps", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_CIERZO_CEVICHE = location("Minor Quest - Need: Cierzo Ceviche", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_FIRE_ELEMENTAL_PARTICLES = location("Minor Quest - Need: Fire Elemental Particles", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_MANAHEART_BASS = location("Minor Quest - Need: Manaheart Bass", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_MANTICORE_TAIL = location("Minor Quest - Need: Manticore Tail", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_SHARK_CARTILAGE = location("Minor Quest - Need: Shark Cartilage", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_SHIELD_GOLEM_SCRAP = location("Minor Quest - Need: Shield Golem Scrap", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_NEED_TOURMALINE = location("Minor Quest - Need: Tourmaline", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_PURIFY_THE_WATER = location("Minor Quest - Purify the Water", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_RED_IDOL = location("Minor Quest - Red Idol", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_SCHOLARS_RANSOM = location("Minor Quest - A Scholar's Ransom", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_SILVER_FOR_THE_SLUMS = location("Minor Quest - Silver for the Slums", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_SKULLS_FOR_CREMEUH = location("Minor Quest - Skulls for Cremeuh", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_STRANGE_APPARITIONS = location("Minor Quest - Strange Apparitions", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_TREASURE_HUNT = location("Minor Quest - Treasure Hunt", OutwardRegionName.MAIN_GAME_AREA)
    QUEST_MINOR_WILLIAM_OF_THE_WISP = location("Minor Quest - William of the Wisp", OutwardRegionName.MAIN_GAME_AREA)

    # comissions

    COMMISSION_ANTIQUE_PLATE_BOOTS = location("Commission - Antique Plate Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_ANTIQUE_PLATE_GARB = location("Commission - Antique Plate Garb", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_ANTIQUE_PLATE_SALLET = location("Commission - Antique Plate Sallet", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_BLUE_SAND_ARMOR = location("Commission - Blue Sand Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_BLUE_SAND_BOOTS = location("Commission - Blue Sand Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_BLUE_SAND_HELM = location("Commission - Blue Sand Helm", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_COPAL_ARMOR = location("Commission - Copal Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_COPAL_BOOTS = location("Commission - Copal Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_COPAL_HELM = location("Commission - Copal Helm", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_PALLADIUM_ARMOR = location("Commission - Palladium Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_PALLADIUM_BOOTS = location("Commission - Palladium Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_PALLADIUM_HELM = location("Commission - Palladium Helm", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_PETRIFIED_WOOD_ARMOR = location("Commission - Petrified Wood Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_PETRIFIED_WOOD_BOOTS = location("Commission - Petrified Wood Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_PETRIFIED_WOOD_HELM = location("Commission - Petrified Wood Helm", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_TENEBROUS_ARMOR = location("Commission - Tenebrous Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_TENEBROUS_BOOTS = location("Commission - Tenebrous Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_TENEBROUS_HELM = location("Commission - Tenebrous Helm", OutwardRegionName.MAIN_GAME_AREA)

    COMMISSION_TSAR_ARMOR = location("Commission - Tsar Armor", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_TSAR_BOOTS = location("Commission - Tsar Boots", OutwardRegionName.MAIN_GAME_AREA)
    COMMISSION_TSAR_HELM = location("Commission - Tsar Helm", OutwardRegionName.MAIN_GAME_AREA)

    # unique items

    SPAWN_ANGLER_SHIELD = location("Item Upgrade - Caldera - Slumbering Shield ", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_BRAND = location("Item Upgrade - Strange Rusted Sword", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_BRASS_WOLF_BACKPACK = location("Spawn - Brass-Wolf Backpack", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_CEREMONIAL_BOW = location("Spawn - Ceremonial Bow", OutwardRegionName.CALDERA)
    SPAWN_COMPASSWOOD_STAFF = location("The Walled Garden - Guardian of the Compass - Compasswood Staff", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_CRACKED_RED_MOON = location("Scarlet Sanctuary - Crimson Avatar - Cracked Red Moon", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_DEPOWERED_BLUDGEON = location("Spawn - De-powered Bludgeon", OutwardRegionName.CALDERA)
    SPAWN_DISTORTED_EXPERIMENT = location("Item Upgrade - Caldera - Experimental Chakram", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_DREAMER_HALBERD = location("Friendly Immaculate Gift - Dreamer Halberd", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_DUTY = location("Item Upgrade - Silkworm's Refuge - Ruined Halberd", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_EXPERIMENTAL_CHAKRAM = location("Spawn - Experimental Chakram", OutwardRegionName.CALDERA)
    SPAWN_FABULOUS_PALLADIUM_SHIELD = location("Spawn - Fabulous Palladium Shield", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_FOSSILIZED_GREATAXE = location("Steam Bath Tunnels - Calygrey Hero - Fossilized Greataxe", OutwardRegionName.CALDERA)
    SPAWN_GEPS_BLADE = location("Item Upgrade - Mysterious Blade/Long Blade", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_GHOST_PARALLEL = location("Item Upgrade - De-Powered Bludgeon", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_GILDED_SHIVER_OF_TRAMONTANE = location("Item Upgrade - Scarred Dagger", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_GRIND = location("Item Upgrade - Oily Cavern - Fossilized Greataxe", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_KRYPTEIA_TOMB_KEY = location("Face of the Ancients - The First Cannibal - Krypteia Tomb Key", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_LIGHT_MENDERS_LEXICON = location("Spawn - Light Mender's Lexicon", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MEFINOS_TRADE_BACKPACK = location("Spawn - Mefino's Trade Backpack", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MERTONS_FIREPOKER = location("Spawn - Merton's Firepoker", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MERTONS_RIBCAGE = location("Spawn - Merton's Ribcage", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MERTONS_SHINBONES = location("Spawn - Merton's Shinbones", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MERTONS_SKULL = location("Spawn - Merton's Skull", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MURMURE = location("Item Upgrade - Ceremonial Bow", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MYSTERIOUS_LONG_BLADE = location("Spawn - Mysterious Long Blade", OutwardRegionName.CALDERA)
    SPAWN_PEARLESCENT_MAIL = location("Ruins of Old Levant - Luke the Pearlescent - Pearlescent Mail", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_PILLAR_GREATHAMMER = location("Spawn - Pillar Greathammer", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_PORCELAIN_FISTS = location("Crumbling Loading Docks - Giant Horror - Porcelain Fists", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_RED_LADYS_DAGGER = location("Face of the Ancients - Red Lady's Altar - Krypteia Tomb Key", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_REVENANT_MOON = location("Item Upgrade - Cracked Red Moon", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_ROTWOOD_STAFF = location("Item Upgrade - Necroplis - Compasswood Staff", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_RUINED_HALBERD = location("Spawn - Ruined Halberd", OutwardRegionName.CALDERA)
    SPAWN_RUSTED_SPEAR = location("Spawn - Rusted Spear", OutwardRegionName.CALDERA)
    SPAWN_SANDROSE = location("Item Upgrade - Eldest Brother - Warm Axe", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARLET_BOOTS = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Boots", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARLET_GEM = location("Spawn - Scarlet Gem", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARLET_LICHS_IDOL = location("Spawn - Scarlet Lich's Idol", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARLET_MASK = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Mask", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARLET_ROBES = location("Scarlet Sanctuary - Crimson Avatar - Scarlet Robes", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCARRED_DAGGER = location("Spawn - Scarred Dagger", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SCEPTER_OF_THE_CRUEL_PRIEST = location("Item Upgrade - Giant's Sauna - Sealed Mace", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SEALED_MACE = location("Old Sirocco - Smelly Sealed Box interaction with forge", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SHRIEK = location("Item Upgrade - Sulphuric Caverns - Rusted Spear", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SKYCROWN_MACE = location("Face of the Ancients - The First Cannibal - Skycrown Mace", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SLUMBERING_SHIELD = location("Spawn - Slumbering Shield", OutwardRegionName.CALDERA)
    SPAWN_SMELLY_SEALED_BOX = location("Spawn - Smelly Sealed Box", OutwardRegionName.CALDERA)
    SPAWN_STARCHILD_CLAYMORE = location("Enmerkar Forest - Royal Manticore - Starchild Claymore", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_STRANGE_RUSTED_SWORD = location("Spawn - Strange Rusted Sword", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_SUNFALL_AXE = location("Spawn - Sunfall Axe", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_THRICE_WROUGHT_HALBERD = location("Spawn - Thrice-Wrought Halberd", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_TOKEBAKICIT = location("Item Upgrade - Unusual Knuckles", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_TSAR_FISTS = location("Spawn - Tsar Fists", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_UNUSUAL_KNUCKLES = location("Spawn - Unusual Knuckles", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_WARM_AXE = location("Spawn - Warm Axe", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_WERLIG_SPEAR = location("Spawn - Werlig Spear", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_WORLDEDGE_GREATAXE = location("Enmerkar Forest - Tyrant of the Hive - Worldedge Greataxe", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_ZHORNS_DEMON_SHIELD = location("Spawn - Zhorn's Demon Shield", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_ZHORNS_GLOWSTONE_DAGGER = location("Spawn - Zhorn's Glowstone Dagger", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_ZHORNS_HUNTING_BACKPACK = location("Spawn - Zhorn's Hunting Backpack", OutwardRegionName.MAIN_GAME_AREA)
    SPAWN_MYRMITAUR_HAVEN_GATE_KEY = location("Myrmitaur Haven - Matriach Myrmitaur - Myrmitaur Haven Gate Key", OutwardRegionName.CALDERA)
