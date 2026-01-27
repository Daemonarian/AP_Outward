from typing import List, NamedTuple

class LocationName:
    COMMISSION_ANTIQUE_PLATE_BOOTS = 'Commission - Antique Plate Boots'
    COMMISSION_ANTIQUE_PLATE_GARB = 'Commission - Antique Plate Garb'
    COMMISSION_ANTIQUE_PLATE_SALLET = 'Commission - Antique Plate Sallet'
    COMMISSION_BLUE_SAND_ARMOR = 'Commission - Blue Sand Armor'
    COMMISSION_BLUE_SAND_BOOTS = 'Commission - Blue Sand Boots'
    COMMISSION_BLUE_SAND_HELM = 'Commission - Blue Sand Helm'
    COMMISSION_COPAL_ARMOR = 'Commission - Copal Armor'
    COMMISSION_COPAL_BOOTS = 'Commission - Copal Boots'
    COMMISSION_COPAL_HELM = 'Commission - Copal Helm'
    COMMISSION_PALLADIUM_ARMOR = 'Commission - Palladium Armor'
    COMMISSION_PALLADIUM_BOOTS = 'Commission - Palladium Boots'
    COMMISSION_PALLADIUM_HELM = 'Commission - Palladium Helm'
    COMMISSION_PETRIFIED_WOOD_ARMOR = 'Commission - Petrified Wood Armor'
    COMMISSION_PETRIFIED_WOOD_BOOTS = 'Commission - Petrified Wood Boots'
    COMMISSION_PETRIFIED_WOOD_HELM = 'Commission - Petrified Wood Helm'
    COMMISSION_TENEBROUS_ARMOR = 'Commission - Tenebrous Armor'
    COMMISSION_TENEBROUS_BOOTS = 'Commission - Tenebrous Boots'
    COMMISSION_TENEBROUS_HELM = 'Commission - Tenebrous Helm'
    COMMISSION_TSAR_ARMOR = 'Commission - Tsar Armor'
    COMMISSION_TSAR_BOOTS = 'Commission - Tsar Boots'
    COMMISSION_TSAR_HELM = 'Commission - Tsar Helm'
    QUEST_MAIN_01 = 'Main Quest 1 - Castaways'
    QUEST_MAIN_02 = 'Main Quest 2 - Call to Adventure'
    QUEST_MAIN_03 = 'Main Quest 3 - Join Faction Quest'
    QUEST_MAIN_04 = 'Main Quest 4 - Faction Quest 1'
    QUEST_MAIN_05 = 'Main Quest 5 - Faction Quest 2'
    QUEST_MAIN_06 = 'Main Quest 6 - Faction Quest 3'
    QUEST_MAIN_07 = 'Main Quest 7 - Faction Quest 4'
    QUEST_MAIN_08 = 'Main Quest 8 - A Fallen City'
    QUEST_MAIN_09 = 'Main Quest 9 - Up The Ladder'
    QUEST_MAIN_10 = 'Main Quest 10 - Stealing Fire'
    QUEST_MAIN_11 = 'Main Quest 11 - Liberate the Sun'
    QUEST_MAIN_12 = 'Main Quest 12 - Vengeful Ouroboros'
    QUEST_MINOR_ACQUIRE_MANA = 'Minor Quest - Acquire Mana'
    QUEST_MINOR_ALCHEMY_COLD_STONE = 'Minor Quest - Alchemy: Cold Stone'
    QUEST_MINOR_ALCHEMY_CRYSTAL_POWDER = 'Minor Quest - Alchemy: Crystal Powder'
    QUEST_MINOR_ARCANE_MACHINE = 'Minor Quest - Arcane Machine'
    QUEST_MINOR_BARREL_MAN = 'Minor Quest - Barrel Man'
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_1 = 'Minor Quest - Beware the Gold Lich - Gold-Lich Spear'
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_2 = 'Minor Quest - Beware the Gold Lich - Gold-Lich Mask'
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_3 = 'Minor Quest - Beware the Gold Lich - Gold-Lich Boots'
    QUEST_MINOR_BEWARE_THE_GOLD_LICH_4 = 'Minor Quest - Beware the Gold Lich - Gold-Lich Armor'
    QUEST_MINOR_BEWARE_THE_JADE_LICH_1 = 'Minor Quest - Beware the Jade Lich - Jade-Lich Staff'
    QUEST_MINOR_BEWARE_THE_JADE_LICH_2 = 'Minor Quest - Beware the Jade Lich - Jade-Lich Boots'
    QUEST_MINOR_BEWARE_THE_JADE_LICH_3 = 'Minor Quest - Beware the Jade Lich - Jade-Lich Mask'
    QUEST_MINOR_BEWARE_THE_JADE_LICH_4 = 'Minor Quest - Beware the Jade Lich - Jade-Lich Robes'
    QUEST_MINOR_BLOODY_BUSINESS = 'Minor Quest - Bloody Business'
    QUEST_MINOR_CRAFT_ANTIQUE_PLATE_GARB_ARMOR = 'Minor Quest - Craft: Antique Plate Garb Armor'
    QUEST_MINOR_CRAFT_BLUE_SAND_ARMOR = 'Minor Quest - Craft: Blue Sand Armor'
    QUEST_MINOR_CRAFT_COPAL_AND_PETRIFIED_ARMOR = 'Minor Quest - Craft: Copal & Petrified Armor'
    QUEST_MINOR_CRAFT_PALLADIUM_ARMOR = 'Minor Quest - Craft: Palladium Armor'
    QUEST_MINOR_CRAFT_TSAR_AND_TENEBROUS_ARMOR = 'Minor Quest - Craft: Tsar & Tenebrous Armor'
    QUEST_MINOR_HELENS_FUNGUS = "Minor Quest - Helen's Fungus"
    QUEST_MINOR_LEDGER_TO_BERG = 'Minor Quest - Ledger to Berg'
    QUEST_MINOR_LEDGER_TO_CIERZO = 'Minor Quest - Ledger to Cierzo'
    QUEST_MINOR_LEDGER_TO_LEVANT = 'Minor Quest - Ledger to Levant'
    QUEST_MINOR_LEDGER_TO_MONSOON = 'Minor Quest - Ledger to Monsoon'
    QUEST_MINOR_LOST_MERCHANT = 'Minor Quest - Lost Merchant'
    QUEST_MINOR_MYRIAD_OF_BONES = 'Minor Quest - A Myriad of Bones'
    QUEST_MINOR_NEED_ANGEL_FOOD_CAKE = 'Minor Quest - Need: Angel Food Cake'
    QUEST_MINOR_NEED_BEAST_GOLEM_SCRAPS = 'Minor Quest - Need: Beast Golem Scraps'
    QUEST_MINOR_NEED_CIERZO_CEVICHE = 'Minor Quest - Need: Cierzo Ceviche'
    QUEST_MINOR_NEED_FIRE_ELEMENTAL_PARTICLES = 'Minor Quest - Need: Fire Elemental Particles'
    QUEST_MINOR_NEED_MANAHEART_BASS = 'Minor Quest - Need: Manaheart Bass'
    QUEST_MINOR_NEED_MANTICORE_TAIL = 'Minor Quest - Need: Manticore Tail'
    QUEST_MINOR_NEED_SHARK_CARTILAGE = 'Minor Quest - Need: Shark Cartilage'
    QUEST_MINOR_NEED_SHIELD_GOLEM_SCRAP = 'Minor Quest - Need: Shield Golem Scrap'
    QUEST_MINOR_NEED_TOURMALINE = 'Minor Quest - Need: Tourmaline'
    QUEST_MINOR_PURIFY_THE_WATER = 'Minor Quest - Purify the Water'
    QUEST_MINOR_RED_IDOL = 'Minor Quest - Red Idol'
    QUEST_MINOR_SCHOLARS_RANSOM = "Minor Quest - A Scholar's Ransom"
    QUEST_MINOR_SILVER_FOR_THE_SLUMS = 'Minor Quest - Silver for the Slums'
    QUEST_MINOR_SKULLS_FOR_CREMEUH = 'Minor Quest - Skulls for Cremeuh'
    QUEST_MINOR_STRANGE_APPARITIONS = 'Minor Quest - Strange Apparitions'
    QUEST_MINOR_TREASURE_HUNT = 'Minor Quest - Treasure Hunt'
    QUEST_MINOR_WILLIAM_OF_THE_WISP = 'Minor Quest - William of the Wisp'
    QUEST_PARALLEL_BLOOD_UNDER_THE_SUN = 'Parallel Quest - Blood Under the Sun'
    QUEST_PARALLEL_PURIFIER = 'Parallel Quest - Purifier'
    QUEST_PARALLEL_RUST_AND_VENGEANCE_1 = 'Parallel Quest - Rust and Vengeance - Rust Lich Armor'
    QUEST_PARALLEL_RUST_AND_VENGEANCE_2 = 'Parallel Quest - Rust and Vengeance - Rust Lich Boots'
    QUEST_PARALLEL_RUST_AND_VENGEANCE_3 = 'Parallel Quest - Rust and Vengeance - Rust Lich Helmet'
    QUEST_PARALLEL_VENDAVEL_QUEST = 'Parallel Quest - Vendavel Quest'
    SPAWN_CEREMONIAL_BOW = 'Spawn - Ceremonial Bow'
    SPAWN_DEPOWERED_BLUDGEON = 'Spawn - De-powered Bludgeon'
    SPAWN_DREAMER_HALBERD = 'Friendly Immaculate Gift - Dreamer Halberd'
    SPAWN_FOSSILIZED_GREATAXE = 'Steam Bath Tunnels - Calygrey Hero - Fossilized Greataxe'
    SPAWN_MERTONS_FIREPOKER = "Spawn - Merton's Firepoker"
    SPAWN_MYSTERIOUS_LONG_BLADE = 'Spawn - Mysterious Long Blade'
    SPAWN_PILLAR_GREATHAMMER = 'Spawn - Pillar Greathammer'
    SPAWN_PORCELAIN_FISTS = 'Crumbling Loading Docks - Giant Horror - Porcelain Fists'
    SPAWN_RUINED_HALBERD = 'Spawn - Ruined Halberd'
    SPAWN_RUSTED_SPEAR = 'Spawn - Rusted Spear'
    SPAWN_SEALED_MACE = 'Old Sirocco - Smelly Sealed Box interaction with forge'
    SPAWN_STRANGE_RUSTED_SWORD = 'Spawn - Strange Rusted Sword'
    SPAWN_SUNFALL_AXE = 'Spawn - Sunfall Axe'
    SPAWN_THRICE_WROUGHT_HALBERD = 'Spawn - Thrice-Wrought Halberd'
    SPAWN_TSAR_FISTS = 'Spawn - Tsar Fists'
    SPAWN_UNUSUAL_KNUCKLES = 'Spawn - Unusual Knuckles'
    SPAWN_WARM_AXE = 'Spawn - Warm Axe'

class LocationData(NamedTuple):
    code: int
    name: str

all_location_data: List[LocationData] = [LocationData(*data) for data in [
    (1, LocationName.COMMISSION_ANTIQUE_PLATE_BOOTS),
    (2, LocationName.COMMISSION_ANTIQUE_PLATE_GARB),
    (3, LocationName.COMMISSION_ANTIQUE_PLATE_SALLET),
    (4, LocationName.COMMISSION_BLUE_SAND_ARMOR),
    (5, LocationName.COMMISSION_BLUE_SAND_BOOTS),
    (6, LocationName.COMMISSION_BLUE_SAND_HELM),
    (7, LocationName.COMMISSION_COPAL_ARMOR),
    (8, LocationName.COMMISSION_COPAL_BOOTS),
    (9, LocationName.COMMISSION_COPAL_HELM),
    (10, LocationName.COMMISSION_PALLADIUM_ARMOR),
    (11, LocationName.COMMISSION_PALLADIUM_BOOTS),
    (12, LocationName.COMMISSION_PALLADIUM_HELM),
    (13, LocationName.COMMISSION_PETRIFIED_WOOD_ARMOR),
    (14, LocationName.COMMISSION_PETRIFIED_WOOD_BOOTS),
    (15, LocationName.COMMISSION_PETRIFIED_WOOD_HELM),
    (16, LocationName.COMMISSION_TENEBROUS_ARMOR),
    (17, LocationName.COMMISSION_TENEBROUS_BOOTS),
    (18, LocationName.COMMISSION_TENEBROUS_HELM),
    (19, LocationName.COMMISSION_TSAR_ARMOR),
    (20, LocationName.COMMISSION_TSAR_BOOTS),
    (21, LocationName.COMMISSION_TSAR_HELM),
    (22, LocationName.QUEST_MAIN_01),
    (23, LocationName.QUEST_MAIN_02),
    (24, LocationName.QUEST_MAIN_03),
    (25, LocationName.QUEST_MAIN_04),
    (26, LocationName.QUEST_MAIN_05),
    (27, LocationName.QUEST_MAIN_06),
    (28, LocationName.QUEST_MAIN_07),
    (29, LocationName.QUEST_MAIN_08),
    (30, LocationName.QUEST_MAIN_09),
    (31, LocationName.QUEST_MAIN_10),
    (32, LocationName.QUEST_MAIN_11),
    (33, LocationName.QUEST_MAIN_12),
    (34, LocationName.QUEST_MINOR_ACQUIRE_MANA),
    (35, LocationName.QUEST_MINOR_ALCHEMY_COLD_STONE),
    (36, LocationName.QUEST_MINOR_ALCHEMY_CRYSTAL_POWDER),
    (37, LocationName.QUEST_MINOR_ARCANE_MACHINE),
    (38, LocationName.QUEST_MINOR_BARREL_MAN),
    (39, LocationName.QUEST_MINOR_BEWARE_THE_GOLD_LICH_1),
    (40, LocationName.QUEST_MINOR_BEWARE_THE_GOLD_LICH_2),
    (41, LocationName.QUEST_MINOR_BEWARE_THE_GOLD_LICH_3),
    (42, LocationName.QUEST_MINOR_BEWARE_THE_GOLD_LICH_4),
    (43, LocationName.QUEST_MINOR_BEWARE_THE_JADE_LICH_1),
    (44, LocationName.QUEST_MINOR_BEWARE_THE_JADE_LICH_2),
    (45, LocationName.QUEST_MINOR_BEWARE_THE_JADE_LICH_3),
    (46, LocationName.QUEST_MINOR_BEWARE_THE_JADE_LICH_4),
    (47, LocationName.QUEST_MINOR_BLOODY_BUSINESS),
    (48, LocationName.QUEST_MINOR_CRAFT_ANTIQUE_PLATE_GARB_ARMOR),
    (49, LocationName.QUEST_MINOR_CRAFT_BLUE_SAND_ARMOR),
    (50, LocationName.QUEST_MINOR_CRAFT_COPAL_AND_PETRIFIED_ARMOR),
    (51, LocationName.QUEST_MINOR_CRAFT_PALLADIUM_ARMOR),
    (52, LocationName.QUEST_MINOR_CRAFT_TSAR_AND_TENEBROUS_ARMOR),
    (53, LocationName.QUEST_MINOR_HELENS_FUNGUS),
    (54, LocationName.QUEST_MINOR_LEDGER_TO_BERG),
    (55, LocationName.QUEST_MINOR_LEDGER_TO_CIERZO),
    (56, LocationName.QUEST_MINOR_LEDGER_TO_LEVANT),
    (57, LocationName.QUEST_MINOR_LEDGER_TO_MONSOON),
    (58, LocationName.QUEST_MINOR_LOST_MERCHANT),
    (59, LocationName.QUEST_MINOR_MYRIAD_OF_BONES),
    (60, LocationName.QUEST_MINOR_NEED_ANGEL_FOOD_CAKE),
    (61, LocationName.QUEST_MINOR_NEED_BEAST_GOLEM_SCRAPS),
    (62, LocationName.QUEST_MINOR_NEED_CIERZO_CEVICHE),
    (63, LocationName.QUEST_MINOR_NEED_FIRE_ELEMENTAL_PARTICLES),
    (64, LocationName.QUEST_MINOR_NEED_MANAHEART_BASS),
    (65, LocationName.QUEST_MINOR_NEED_MANTICORE_TAIL),
    (66, LocationName.QUEST_MINOR_NEED_SHARK_CARTILAGE),
    (67, LocationName.QUEST_MINOR_NEED_SHIELD_GOLEM_SCRAP),
    (68, LocationName.QUEST_MINOR_NEED_TOURMALINE),
    (69, LocationName.QUEST_MINOR_PURIFY_THE_WATER),
    (70, LocationName.QUEST_MINOR_RED_IDOL),
    (71, LocationName.QUEST_MINOR_SCHOLARS_RANSOM),
    (72, LocationName.QUEST_MINOR_SILVER_FOR_THE_SLUMS),
    (73, LocationName.QUEST_MINOR_SKULLS_FOR_CREMEUH),
    (74, LocationName.QUEST_MINOR_STRANGE_APPARITIONS),
    (75, LocationName.QUEST_MINOR_TREASURE_HUNT),
    (76, LocationName.QUEST_MINOR_WILLIAM_OF_THE_WISP),
    (77, LocationName.QUEST_PARALLEL_BLOOD_UNDER_THE_SUN),
    (78, LocationName.QUEST_PARALLEL_PURIFIER),
    (79, LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_1),
    (80, LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_2),
    (81, LocationName.QUEST_PARALLEL_RUST_AND_VENGEANCE_3),
    (82, LocationName.QUEST_PARALLEL_VENDAVEL_QUEST),
    (83, LocationName.SPAWN_CEREMONIAL_BOW),
    (84, LocationName.SPAWN_DEPOWERED_BLUDGEON),
    (85, LocationName.SPAWN_DREAMER_HALBERD),
    (86, LocationName.SPAWN_FOSSILIZED_GREATAXE),
    (87, LocationName.SPAWN_MERTONS_FIREPOKER),
    (88, LocationName.SPAWN_MYSTERIOUS_LONG_BLADE),
    (89, LocationName.SPAWN_PILLAR_GREATHAMMER),
    (90, LocationName.SPAWN_PORCELAIN_FISTS),
    (91, LocationName.SPAWN_RUINED_HALBERD),
    (92, LocationName.SPAWN_RUSTED_SPEAR),
    (93, LocationName.SPAWN_SEALED_MACE),
    (94, LocationName.SPAWN_STRANGE_RUSTED_SWORD),
    (95, LocationName.SPAWN_SUNFALL_AXE),
    (96, LocationName.SPAWN_THRICE_WROUGHT_HALBERD),
    (97, LocationName.SPAWN_TSAR_FISTS),
    (98, LocationName.SPAWN_UNUSUAL_KNUCKLES),
    (99, LocationName.SPAWN_WARM_AXE),
]]
