from typing import List, NamedTuple

from BaseClasses import ItemClassification

class ItemName:
    ANTIQUE_PLATE_BOOTS = 'Antique Plate Boots'
    ANTIQUE_PLATE_GARB = 'Antique Plate Garb'
    ANTIQUE_PLATE_SALLET = 'Antique Plate Sallet'
    BLUE_SAND_ARMOR = 'Blue Sand Armor'
    BLUE_SAND_BOOTS = 'Blue Sand Boots'
    BLUE_SAND_HELM = 'Blue Sand Helm'
    CEREMONIAL_BOW = 'Ceremonial Bow'
    COPAL_ARMOR = 'Copal Armor'
    COPAL_BOOTS = 'Copal Boots'
    COPAL_HELM = 'Copal Helm'
    CURSE_HEX = 'Curse Hex'
    DEPOWERED_BLUDGEON = 'De-powered Bludgeon'
    DREAMER_HALBERD = 'Dreamer Halberd'
    FOSSILIZED_GREATAXE = 'Fossilized Greataxe'
    GOLD_LICH_ARMOR = 'Gold-Lich Armor'
    GOLD_LICH_BOOTS = 'Gold-Lich Boots'
    GOLD_LICH_MASK = 'Gold-Lich Mask'
    GOLD_LICH_SPEAR = 'Gold-Lich Spear'
    JADE_LICH_BOOTS = 'Jade-Lich Boots'
    JADE_LICH_MASK = 'Jade-Lich Mask'
    JADE_LICH_ROBES = 'Jade-Lich Robes'
    JADE_LICH_STAFF = 'Jade-Lich Staff'
    LIGHT_MENDERS_BACKPACK = "Light Mender's Backpack"
    MERTONS_FIREPOKER = "Merton's Firepoker"
    MYSTERIOUS_CHAKRAM = 'Mysterious Chakram'
    MYSTERIOUS_LONG_BLADE = 'Mysterious Long Blade'
    ORNATE_BONE_SHIELD = 'Ornate Bone Shield'
    PALLADIUM_ARMOR = 'Palladium Armor'
    PALLADIUM_BOOTS = 'Palladium Boots'
    PALLADIUM_HELM = 'Palladium Helm'
    PETRIFIED_WOOD_ARMOR = 'Petrified Wood Armor'
    PETRIFIED_WOOD_BOOTS = 'Petrified Wood Boots'
    PETRIFIED_WOOD_HELM = 'Petrified Wood Helm'
    PILLAR_GREATHAMMER = 'Pillar Greathammer'
    PORCELAIN_FISTS = 'Porcelain Fists'
    POSSESSED_SKILL = 'Possessed'
    QUEST_LICENSE = 'Progressive Quest License'
    RUINED_HALBERD = 'Ruined Halberd'
    RUSTED_SPEAR = 'Rusted Spear'
    RUST_LICH_ARMOR = 'Rust Lich Armor'
    RUST_LICH_BOOTS = 'Rust Lich Boots'
    RUST_LICH_HELMET = 'Rust Lich Helmet'
    SEALED_MACE = 'Sealed Mace'
    SILVER_CURRENCY = '50x Silver'
    STRANGE_RUSTED_SWORD = 'Strange Rusted Sword'
    SUNFALL_AXE = 'Sunfall Axe'
    TENEBROUS_ARMOR = 'Tenebrous Armor'
    TENEBROUS_BOOTS = 'Tenebrous Boots'
    TENEBROUS_HELM = 'Tenebrous Helm'
    THRICE_WROUGHT_HALBERD = 'Thrice-Wrought Halberd'
    TSAR_ARMOR = 'Tsar Armor'
    TSAR_BOOTS = 'Tsar Boots'
    TSAR_FISTS = 'Tsar Fists'
    TSAR_HELM = 'Tsar Helm'
    UNUSUAL_KNUCKLES = 'Unusual Knuckles'
    WARM_AXE = 'Warm Axe'
    WILL_O_WISP = 'The Will O Wisp'

class ItemData(NamedTuple):
    code: int
    name: str
    classification: ItemClassification
    count: int

all_item_data: List[ItemData] = [ItemData(*data) for data in [
    (1, ItemName.ANTIQUE_PLATE_BOOTS, ItemClassification.useful, 1),
    (2, ItemName.ANTIQUE_PLATE_GARB, ItemClassification.useful, 1),
    (3, ItemName.ANTIQUE_PLATE_SALLET, ItemClassification.useful, 1),
    (4, ItemName.BLUE_SAND_ARMOR, ItemClassification.useful, 1),
    (5, ItemName.BLUE_SAND_BOOTS, ItemClassification.useful, 1),
    (6, ItemName.BLUE_SAND_HELM, ItemClassification.useful, 1),
    (7, ItemName.CEREMONIAL_BOW, ItemClassification.useful, 1),
    (8, ItemName.COPAL_ARMOR, ItemClassification.useful, 1),
    (9, ItemName.COPAL_BOOTS, ItemClassification.useful, 1),
    (10, ItemName.COPAL_HELM, ItemClassification.useful, 1),
    (11, ItemName.CURSE_HEX, ItemClassification.useful, 1),
    (12, ItemName.DEPOWERED_BLUDGEON, ItemClassification.useful, 1),
    (13, ItemName.DREAMER_HALBERD, ItemClassification.useful, 1),
    (14, ItemName.FOSSILIZED_GREATAXE, ItemClassification.useful, 1),
    (15, ItemName.GOLD_LICH_ARMOR, ItemClassification.useful, 1),
    (16, ItemName.GOLD_LICH_BOOTS, ItemClassification.useful, 1),
    (17, ItemName.GOLD_LICH_MASK, ItemClassification.useful, 1),
    (18, ItemName.GOLD_LICH_SPEAR, ItemClassification.useful, 1),
    (19, ItemName.JADE_LICH_BOOTS, ItemClassification.useful, 1),
    (20, ItemName.JADE_LICH_MASK, ItemClassification.useful, 1),
    (21, ItemName.JADE_LICH_ROBES, ItemClassification.useful, 1),
    (22, ItemName.JADE_LICH_STAFF, ItemClassification.useful, 1),
    (23, ItemName.LIGHT_MENDERS_BACKPACK, ItemClassification.useful, 1),
    (24, ItemName.MERTONS_FIREPOKER, ItemClassification.useful, 1),
    (25, ItemName.MYSTERIOUS_CHAKRAM, ItemClassification.useful, 1),
    (26, ItemName.MYSTERIOUS_LONG_BLADE, ItemClassification.useful, 1),
    (27, ItemName.ORNATE_BONE_SHIELD, ItemClassification.useful, 1),
    (28, ItemName.PALLADIUM_ARMOR, ItemClassification.useful, 1),
    (29, ItemName.PALLADIUM_BOOTS, ItemClassification.useful, 1),
    (30, ItemName.PALLADIUM_HELM, ItemClassification.useful, 1),
    (31, ItemName.PETRIFIED_WOOD_ARMOR, ItemClassification.useful, 1),
    (32, ItemName.PETRIFIED_WOOD_BOOTS, ItemClassification.useful, 1),
    (33, ItemName.PETRIFIED_WOOD_HELM, ItemClassification.useful, 1),
    (34, ItemName.PILLAR_GREATHAMMER, ItemClassification.useful, 1),
    (35, ItemName.PORCELAIN_FISTS, ItemClassification.useful, 1),
    (36, ItemName.POSSESSED_SKILL, ItemClassification.useful, 1),
    (37, ItemName.QUEST_LICENSE, ItemClassification.progression, 10),
    (38, ItemName.RUINED_HALBERD, ItemClassification.useful, 1),
    (39, ItemName.RUST_LICH_ARMOR, ItemClassification.useful, 1),
    (40, ItemName.RUST_LICH_BOOTS, ItemClassification.useful, 1),
    (41, ItemName.RUST_LICH_HELMET, ItemClassification.useful, 1),
    (42, ItemName.RUSTED_SPEAR, ItemClassification.useful, 1),
    (43, ItemName.SEALED_MACE, ItemClassification.useful, 1),
    (44, ItemName.SILVER_CURRENCY, ItemClassification.filler, 1),
    (45, ItemName.STRANGE_RUSTED_SWORD, ItemClassification.useful, 1),
    (46, ItemName.SUNFALL_AXE, ItemClassification.useful, 1),
    (47, ItemName.TENEBROUS_ARMOR, ItemClassification.useful, 1),
    (48, ItemName.TENEBROUS_BOOTS, ItemClassification.useful, 1),
    (49, ItemName.TENEBROUS_HELM, ItemClassification.useful, 1),
    (50, ItemName.THRICE_WROUGHT_HALBERD, ItemClassification.useful, 1),
    (51, ItemName.TSAR_ARMOR, ItemClassification.useful, 1),
    (52, ItemName.TSAR_BOOTS, ItemClassification.useful, 1),
    (53, ItemName.TSAR_FISTS, ItemClassification.useful, 1),
    (54, ItemName.TSAR_HELM, ItemClassification.useful, 1),
    (55, ItemName.UNUSUAL_KNUCKLES, ItemClassification.useful, 1),
    (56, ItemName.WARM_AXE, ItemClassification.useful, 1),
    (57, ItemName.WILL_O_WISP, ItemClassification.useful, 1),
]]
