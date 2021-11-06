﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Rnd = UnityEngine.Random;
using KModkit;

public class ThreeDTapCodeScript : MonoBehaviour
{
    public KMBombModule Module;
    public KMBombInfo BombInfo;
    public KMAudio Audio;
    public KMSelectable Sel;
    public KMRuleSeedable RuleSeedable;

    private int _moduleId;
    private static int _moduleIdCounter = 1;
    private bool _moduleSolved;

    private static readonly string[] WordList = {
            "ABACK", "ABBEY", "ABBOT", "ABIDE", "ABORT", "ABOUT", "ABOVE", "ABUSE", "ABYSS", "ACIDS", "ACORN", "ACRES", "ACTED", "ACTOR", "ACUTE", "ADAPT", "ADDED", "ADDER", "ADDLE", "ADIEU", "ADIOS", "ADMIN", "ADMIT", "ADOPT", "ADORE", "ADORN", "ADULT", "AFFIX", "AFTER", "AGAIN", "AGENT", "AGILE", "AGING", "AGONY", "AGORA", "AGREE", "AHEAD", "AIDED", "AIMED", "AIOLI", "AIRED", "AISLE", "ALARM", "ALBUM", "ALERT", "ALGAE", "ALIAS", "ALIBI", "ALIEN", "ALIGN", "ALIKE", "ALIVE", "ALLAY", "ALLEY", "ALLOT", "ALLOW", "ALLOY", "ALOFT", "ALONE", "ALONG", "ALOOF", "ALOUD", "ALPHA", "ALTAR", "ALTER", "AMASS", "AMAZE", "AMBER", "AMBLE", "AMEND", "AMISH", "AMISS", "AMONG", "AMPLE", "AMUSE", "ANGEL", "ANGER", "ANGLE", "ANGLO", "ANGRY", "ANGST", "ANIME", "ANION", "ANISE", "ANKLE", "ANNEX", "ANNOY", "ANNUL", "ANTIC", "ANVIL", "AORTA", "APART", "APNEA", "APPLE", "APPLY", "APRON", "AREAS", "ARENA", "ARGUE", "ARISE", "ARMED", "ARMOR", "AROMA", "AROSE", "ARRAY", "ARROW", "ARSON", "ASHEN", "ASHES", "ASIAN", "ASIDE", "ASKED", "ASSAY", "ASSET", "ASTER", "ASTIR", "ATOLL", "ATOMS", "ATONE", "ATTIC", "AUDIO", "AUDIT", "AUGUR", "AUNTY", "AVAIL", "AVIAN", "AVOID", "AWAIT", "AWAKE", "AWARD", "AWARE", "AWASH", "AWFUL", "AWOKE", "AXIAL", "AXIOM", "AXION", "AZTEC",
            "BACKS", "BACON", "BADGE", "BADLY", "BAKED", "BAKER", "BALLS", "BANDS", "BANKS", "BARGE", "BARON", "BASAL", "BASED", "BASES", "BASIC", "BASIL", "BASIN", "BASIS", "BATCH", "BATHS", "BATTY", "BEACH", "BEADS", "BEAMS", "BEANS", "BEARD", "BEARS", "BEAST", "BEECH", "BEERS", "BEGAN", "BEGIN", "BEGUN", "BEING", "BELLS", "BELLY", "BELOW", "BELTS", "BENCH", "BERRY", "BIBLE", "BIDET", "BIGHT", "BIKES", "BILGE", "BILLS", "BINGE", "BINGO", "BIOME", "BIRCH", "BIRDS", "BIRTH", "BISON", "BITCH", "BITER", "BLACK", "BLADE", "BLAME", "BLAND", "BLANK", "BLARE", "BLAST", "BLAZE", "BLEAK", "BLEAT", "BLEED", "BLEEP", "BLEND", "BLESS", "BLIMP", "BLIND", "BLING", "BLINK", "BLISS", "BLITZ", "BLOCK", "BLOKE", "BLOND", "BLOOD", "BLOOM", "BLOOP", "BLOWN", "BLOWS", "BLUES", "BLUFF", "BLUNT", "BLUSH", "BOARD", "BOATS", "BOGGY", "BOGUS", "BOLTS", "BOMBS", "BONDS", "BONED", "BONER", "BONES", "BONNY", "BONUS", "BOOKS", "BOOST", "BOOTH", "BOOTS", "BORAX", "BORED", "BORER", "BORNE", "BORON", "BOTCH", "BOUGH", "BOULE", "BOUND", "BOWED", "BOWEL", "BOWLS", "BOXED", "BOXER", "BOXES", "BRACE", "BRAID", "BRAIN", "BRAKE", "BRAND", "BRASH", "BRASS", "BRAVE", "BRAWL", "BRAWN", "BRAZE", "BREAD", "BREAK", "BREAM", "BREED", "BRIAR", "BRIBE", "BRICK", "BRIDE", "BRIEF", "BRIER", "BRINE", "BRING", "BRINK", "BRINY", "BRISK", "BROAD", "BROIL", "BROKE", "BROOK", "BROOM", "BROTH", "BROWN", "BROWS", "BRUNT", "BRUSH", "BRUTE", "BUCKS", "BUDDY", "BUDGE", "BUGGY", "BUILD", "BUILT", "BULBS", "BULGE", "BULKY", "BULLS", "BUMPY", "BUNCH", "BUNNY", "BURNS", "BURNT", "BURST", "BUSES", "BUYER", "BUZZY", "BYLAW", "BYWAY",
            "CABBY", "CABIN", "CABLE", "CACHE", "CAIRN", "CAKES", "CALLS", "CALVE", "CALYX", "CAMPS", "CAMPY", "CANAL", "CANDY", "CANED", "CANNY", "CANOE", "CANON", "CARDS", "CARED", "CARER", "CARES", "CARGO", "CAROL", "CARRY", "CARVE", "CASED", "CASES", "CASTE", "CATCH", "CATER", "CAULK", "CAUSE", "CAVES", "CEASE", "CEDED", "CELLS", "CENTS", "CHAFE", "CHAFF", "CHAIN", "CHAIR", "CHALK", "CHAMP", "CHANT", "CHAOS", "CHAPS", "CHARM", "CHART", "CHARY", "CHASE", "CHASM", "CHEAP", "CHEAT", "CHECK", "CHEEK", "CHEER", "CHEMO", "CHESS", "CHEST", "CHICK", "CHIDE", "CHIEF", "CHILD", "CHILI", "CHILL", "CHIME", "CHINA", "CHIPS", "CHOIR", "CHORD", "CHORE", "CHOSE", "CHUCK", "CHUNK", "CHUTE", "CIDER", "CIGAR", "CINCH", "CITED", "CITES", "CIVET", "CIVIC", "CIVIL", "CLADE", "CLAIM", "CLANK", "CLASH", "CLASS", "CLAWS", "CLEAN", "CLEAR", "CLEAT", "CLERK", "CLICK", "CLIFF", "CLIMB", "CLING", "CLOAK", "CLOCK", "CLONE", "CLOSE", "CLOTH", "CLOUD", "CLOUT", "CLOVE", "CLOWN", "CLUBS", "CLUCK", "CLUES", "CLUNG", "CLUNK", "COACH", "COAST", "COATS", "COCOA", "CODES", "COINS", "COLIC", "COLON", "COLOR", "COMAL", "COMES", "COMIC", "COMMA", "CONCH", "CONIC", "CORAL", "CORGI", "CORNY", "CORPS", "COSTS", "COTTA", "COUCH", "COUGH", "COULD", "COUNT", "COURT", "COVEN", "COVER", "COYLY", "CRACK", "CRAFT", "CRANE", "CRANK", "CRASH", "CRASS", "CRATE", "CRAVE", "CRAWL", "CRAZY", "CREAK", "CREAM", "CREED", "CREEK", "CREPT", "CREST", "CREWS", "CRIED", "CRIES", "CRIME", "CRISP", "CRONE", "CROPS", "CROSS", "CROWD", "CROWN", "CRUDE", "CRUEL", "CRUSH", "CRUST", "CRYPT", "CUBAN", "CUBBY", "CUBIC", "CUBIT", "CUMIN", "CURLS", "CURLY", "CURRY", "CURSE", "CURVE", "CUTIE", "CYCLE", "CYNIC", "CZECH",
            "DACHA", "DADDY", "DAILY", "DAIRY", "DAISY", "DALLY", "DANCE", "DARED", "DATED", "DATES", "DATUM", "DEALS", "DEALT", "DEATH", "DEBIT", "DEBTS", "DEBUG", "DEBUT", "DECAF", "DECAL", "DECAY", "DECOR", "DECOY", "DEEDS", "DEIST", "DEITY", "DELAY", "DELFT", "DELVE", "DEMUR", "DENIM", "DENSE", "DEPOT", "DEPTH", "DERBY", "DERRY", "DESKS", "DETER", "DETOX", "DEUCE", "DEVIL", "DIARY", "DICED", "DIETS", "DIGIT", "DIMLY", "DINAR", "DINER", "DINGY", "DIRTY", "DISCO", "DISCS", "DISKS", "DITCH", "DITTY", "DITZY", "DIVAN", "DIVED", "DIVER", "DIVOT", "DIVVY", "DIZZY", "DOCKS", "DODGE", "DODGY", "DOGGY", "DOGMA", "DOING", "DOLLS", "DOMED", "DONOR", "DONUT", "DOORS", "DORIC", "DOSED", "DOSES", "DOTTY", "DOUBT", "DOUGH", "DOUSE", "DOWNS", "DOZEN", "DRAFT", "DRAIN", "DRAMA", "DRANK", "DRAWN", "DRAWS", "DREAD", "DREAM", "DRESS", "DRIED", "DRIER", "DRIFT", "DRILL", "DRILY", "DRINK", "DRIVE", "DROLL", "DRONE", "DROPS", "DROVE", "DROWN", "DRUGS", "DRUMS", "DRUNK", "DRYER", "DUCAT", "DUCHY", "DUCKS", "DUMMY", "DUNCE", "DUNES", "DUSTY", "DUTCH", "DUVET", "DWARF", "DWELL", "DYING",
            "EAGER", "EAGLE", "EARED", "EARLY", "EARTH", "EASED", "EASEL", "EATEN", "ECLAT", "EDEMA", "EDGES", "EDICT", "EDIFY", "EERIE", "EGRET", "EIDER", "EIGHT", "ELATE", "ELBOW", "ELDER", "ELECT", "ELIDE", "ELITE", "ELUDE", "ELVES", "EMCEE", "EMOTE", "EMPTY", "ENACT", "ENDED", "ENEMA", "ENEMY", "ENJOY", "ENNUI", "ENSUE", "ENTER", "ENTRY", "ENVOY", "EQUAL", "EQUIP", "ERASE", "ERECT", "ERROR", "ESSAY", "ETHIC", "ETHOS", "ETUDE", "EVADE", "EVENT", "EVERY", "EVICT", "EXACT", "EXALT", "EXAMS", "EXERT", "EXILE", "EXIST", "EXTRA", "EXUDE",
            "FACED", "FACES", "FACTS", "FADED", "FAILS", "FAINT", "FAIRS", "FAIRY", "FAITH", "FALLS", "FALSE", "FAMED", "FANCY", "FARES", "FARMS", "FATAL", "FATED", "FATTY", "FATWA", "FAULT", "FAUNA", "FAVOR", "FEARS", "FEAST", "FECAL", "FEELS", "FEINT", "FELLA", "FENCE", "FERRY", "FETAL", "FETCH", "FEVER", "FEWER", "FIBER", "FIBRE", "FIELD", "FIERY", "FIFTH", "FIFTY", "FIGHT", "FILCH", "FILED", "FILES", "FILET", "FILLE", "FILLS", "FILLY", "FILMS", "FILMY", "FILTH", "FINAL", "FINDS", "FINED", "FINER", "FINES", "FINNY", "FIRED", "FIRES", "FIRMS", "FIRST", "FISTS", "FIVER", "FIXED", "FLAGS", "FLAIL", "FLAIR", "FLAME", "FLANK", "FLARE", "FLASH", "FLASK", "FLATS", "FLAWS", "FLEET", "FLESH", "FLIES", "FLING", "FLIRT", "FLOAT", "FLOCK", "FLOOD", "FLOOR", "FLORA", "FLOUR", "FLOUT", "FLOWN", "FLOWS", "FLUID", "FLUNG", "FLUNK", "FLUSH", "FLUTE", "FLYBY", "FOCAL", "FOCUS", "FOGGY", "FOIST", "FOLDS", "FOLIC", "FOLIO", "FOLKS", "FOLLY", "FONTS", "FOODS", "FOOLS", "FORAY", "FORCE", "FORGE", "FORGO", "FORMS", "FORTE", "FORTH", "FORTY", "FORUM", "FOUND", "FOUNT", "FOURS", "FOVEA", "FOXES", "FOYER", "FRAIL", "FRAME", "FRANC", "FRANK", "FRAUD", "FREAK", "FREED", "FRESH", "FRIED", "FRILL", "FRISK", "FROGS", "FRONT", "FROST", "FROWN", "FROZE", "FRUIT", "FUDGE", "FUELS", "FULLY", "FUMES", "FUNDS", "FUNNY", "FUSED", "FUTON", "FUZZY",
            "GAINS", "GAMES", "GANGS", "GASES", "GATES", "GAUGE", "GAZED", "GEESE", "GENES", "GENIE", "GENRE", "GENUS", "GHOST", "GHOUL", "GIANT", "GIDDY", "GIFTS", "GIMPY", "GIRLS", "GIRLY", "GIRTH", "GIVEN", "GIVES", "GIZMO", "GLAND", "GLARE", "GLASS", "GLEAM", "GLEAN", "GLIAL", "GLIDE", "GLINT", "GLOBE", "GLOOM", "GLORY", "GLOSS", "GLOVE", "GLUED", "GLUON", "GOALS", "GOATS", "GOING", "GOLLY", "GOODS", "GOOFY", "GOOPY", "GOOSE", "GORGE", "GRACE", "GRADE", "GRAFT", "GRAIN", "GRAMS", "GRAND", "GRANT", "GRAPE", "GRAPH", "GRASP", "GRASS", "GRATE", "GRAVE", "GRAVY", "GREAT", "GREED", "GREEK", "GREEN", "GREET", "GRIEF", "GRILL", "GRIME", "GRIMY", "GRIND", "GRIPS", "GROIN", "GROOM", "GROSS", "GROUP", "GROUT", "GROWN", "GROWS", "GRUEL", "GRUMP", "GRUNT", "GUANO", "GUARD", "GUAVA", "GUESS", "GUEST", "GUIDE", "GUILD", "GUILT", "GUISE", "GULLS", "GULLY", "GUMMY", "GUNKY", "GUNNY", "GUSHY", "GUSTY", "GUTSY", "GYPSY", "GYRUS",
            "HABIT", "HAIKU", "HAIRS", "HAIRY", "HALAL", "HALLS", "HALVE", "HAMMY", "HANDS", "HANDY", "HANGS", "HAPPY", "HARDY", "HAREM", "HARPY", "HARSH", "HASTE", "HASTY", "HATCH", "HATED", "HATES", "HAUNT", "HAVEN", "HAVOC", "HAZEL", "HEADS", "HEADY", "HEARD", "HEARS", "HEART", "HEATH", "HEAVE", "HEAVY", "HEDGE", "HEELS", "HEFTY", "HEIRS", "HEIST", "HELIX", "HELLO", "HELPS", "HENCE", "HENRY", "HERBS", "HERDS", "HILLS", "HILLY", "HINDU", "HINGE", "HINTS", "HIPPO", "HIRED", "HITCH", "HOBBY", "HOIST", "HOLDS", "HOLES", "HOLLY", "HOMED", "HOMES", "HONEY", "HONOR", "HOOKS", "HOPED", "HOPES", "HORNS", "HORSE", "HOSEL", "HOSTS", "HOTEL", "HOTLY", "HOUND", "HOURS", "HOUSE", "HUBBY", "HUGGY", "HULLO", "HUMAN", "HUMID", "HUMOR", "HUMUS", "HURRY", "HURTS", "HUSKY", "HYENA", "HYMNS",
            "ICHOR", "ICILY", "ICING", "ICONS", "IDEAL", "IDEAS", "IDIOM", "IDIOT", "IDLED", "IDYLL", "IGLOO", "ILIAC", "ILIUM", "IMAGE", "IMAGO", "IMBUE", "IMPLY", "INANE", "INCUS", "INDEX", "INDIA", "INDIE", "INERT", "INFER", "INFRA", "INGOT", "INLAY", "INLET", "INNER", "INPUT", "INTRO", "IRISH", "IRONY", "ISLET", "ISSUE", "ITCHY", "ITEMS", "IVORY",
            "JAPAN", "JEANS", "JELLY", "JEWEL", "JOINS", "JOINT", "JOKER", "JOKES", "JOLLY", "JOULE", "JOUST", "JUDGE", "JUICE", "JUICY", "JUMBO", "JUMPS", "JUNTA",
            "KABOB", "KANJI", "KARAT", "KARMA", "KAYAK", "KAZOO", "KEEPS", "KICKS", "KIDDO", "KILLS", "KINDA", "KINDS", "KINGS", "KITTY", "KNAVE", "KNEAD", "KNEEL", "KNEES", "KNELT", "KNIFE", "KNOBS", "KNOCK", "KNOLL", "KNOTS", "KNOWN", "KNOWS", "KOALA", "KUDOS", "KUDZU",
            "LABEL", "LABOR", "LACED", "LACKS", "LADLE", "LAGER", "LAITY", "LAKES", "LAMBS", "LAMPS", "LANDS", "LANES", "LAPIN", "LAPSE", "LARGE", "LARVA", "LASER", "LASSO", "LASTS", "LATCH", "LATER", "LATHE", "LATIN", "LATTE", "LAUGH", "LAWNS", "LAYER", "LAYUP", "LEACH", "LEADS", "LEAFY", "LEAKY", "LEANT", "LEAPT", "LEARN", "LEASE", "LEASH", "LEAST", "LEAVE", "LEDGE", "LEECH", "LEGAL", "LEGGY", "LEMMA", "LEMON", "LEMUR", "LEVEL", "LEVER", "LIANA", "LIBEL", "LIDAR", "LIEGE", "LIFTS", "LIGHT", "LIKED", "LIKEN", "LIKES", "LILAC", "LIMBO", "LIMBS", "LIMIT", "LINED", "LINEN", "LINER", "LINES", "LINGO", "LINKS", "LIONS", "LIPID", "LISTS", "LITER", "LITRE", "LIVED", "LIVEN", "LIVER", "LIVES", "LIVID", "LLAMA", "LOADS", "LOANS", "LOBBY", "LOCAL", "LOCKS", "LOCUS", "LODGE", "LOFTY", "LOGIC", "LOGIN", "LOGON", "LOLLY", "LONER", "LOOKS", "LOONY", "LOOPS", "LOOPY", "LOOSE", "LORDS", "LORRY", "LOSER", "LOSES", "LOTTO", "LOTUS", "LOUSE", "LOUSY", "LOVED", "LOVER", "LOVES", "LOWER", "LOYAL", "LUCID", "LUCKY", "LUCRE", "LUMEN", "LUMPS", "LUMPY", "LUNAR", "LUNCH", "LUNGE", "LUNGS", "LUSTY", "LYING", "LYMPH", "LYNCH", "LYRIC",
            "MACHO", "MADAM", "MADLY", "MAGIC", "MAGMA", "MAINS", "MAIZE", "MAJOR", "MAKER", "MAKES", "MALES", "MAMBO", "MANGO", "MANGY", "MANIA", "MANIC", "MANLY", "MANOR", "MAPLE", "MARCH", "MARKS", "MARRY", "MARSH", "MASKS", "MATCH", "MATED", "MATES", "MATHS", "MATTE", "MAVEN", "MAXIM", "MAYAN", "MAYBE", "MAYOR", "MEALS", "MEANS", "MEANT", "MEATY", "MEDAL", "MEDIA", "MEDIC", "MEETS", "MELON", "MENUS", "MERCY", "MERGE", "MERIT", "MERRY", "MESON", "MESSY", "METAL", "METER", "METRE", "MICRO", "MIDST", "MIGHT", "MILES", "MILLS", "MIMIC", "MINCE", "MINDS", "MINED", "MINER", "MINES", "MINOR", "MINTY", "MINUS", "MIRED", "MIRTH", "MISTY", "MITRE", "MIXED", "MIXER", "MODEL", "MODEM", "MODES", "MOGUL", "MOIST", "MOLAR", "MOLDY", "MOLES", "MONEY", "MONKS", "MONTH", "MOODS", "MOONY", "MOORS", "MOOSE", "MORAL", "MORAY", "MORPH", "MOTEL", "MOTIF", "MOTOR", "MOTTO", "MOULD", "MOUND", "MOUNT", "MOUSE", "MOUTH", "MOVED", "MOVER", "MOVES", "MOVIE", "MUCUS", "MUDDY", "MUMMY", "MUNCH", "MURKY", "MUSED", "MUSIC", "MUSTY", "MUTED", "MUZZY", "MYTHS",
            "NACHO", "NADIR", "NAILS", "NAIVE", "NAKED", "NAMED", "NAMES", "NANNY", "NASAL", "NASTY", "NATAL", "NATTY", "NAVAL", "NAVEL", "NEATH", "NECKS", "NEEDS", "NEEDY", "NEIGH", "NERVE", "NESTS", "NEVER", "NEWER", "NEWLY", "NEXUS", "NICER", "NICHE", "NIECE", "NIFTY", "NIGHT", "NINJA", "NINTH", "NITRO", "NOBLE", "NOBLY", "NODES", "NOISE", "NOISY", "NOMAD", "NOMES", "NONCE", "NOOSE", "NORMS", "NORTH", "NOSES", "NOTCH", "NOTED", "NOTES", "NOVEL", "NUDGE", "NURSE", "NUTTY", "NYLON", "NYMPH",
            "OASIS", "OCCUR", "OCEAN", "ODDLY", "ODOUR", "OFFER", "OFTEN", "OILED", "OLDER", "OLDIE", "OLIVE", "ONION", "ONSET", "OOMPH", "OPENS", "OPERA", "OPINE", "OPIUM", "OPTIC", "ORBIT", "ORDER", "ORGAN", "OTHER", "OTTER", "OUGHT", "OUNCE", "OUTDO", "OUTER", "OVERS", "OVERT", "OVOID", "OVULE", "OWNED", "OWNER", "OXBOW", "OXIDE", "OZONE",
            "PACKS", "PADDY", "PAGES", "PAINS", "PAINT", "PAIRS", "PALMS", "PANDA", "PANEL", "PANIC", "PANTS", "PAPAL", "PAPER", "PARKS", "PARTS", "PARTY", "PASTA", "PASTE", "PATCH", "PATHS", "PATIO", "PAUSE", "PEACE", "PEACH", "PEAKS", "PEARL", "PEARS", "PEDAL", "PEERS", "PENAL", "PENCE", "PENNY", "PERIL", "PESTS", "PETTY", "PHASE", "PHONE", "PHOTO", "PIANO", "PICKS", "PIECE", "PIERS", "PIGGY", "PILAF", "PILED", "PILES", "PILLS", "PILOT", "PINCH", "PINTS", "PIOUS", "PIPES", "PISTE", "PITCH", "PIVOT", "PIXEL", "PIXIE", "PIZZA", "PLACE", "PLAIN", "PLAIT", "PLANE", "PLANK", "PLANS", "PLANT", "PLATE", "PLAYS", "PLAZA", "PLEAD", "PLEAS", "PLEAT", "PLOTS", "PLUMB", "PLUME", "PLUMP", "POEMS", "POETS", "POINT", "POKER", "POLAR", "POLES", "POLIO", "POLLS", "POLYP", "PONDS", "POOLS", "PORCH", "PORES", "PORTS", "POSED", "POSES", "POSIT", "POSTS", "POUCH", "POUND", "POWER", "PREEN", "PRESS", "PRICE", "PRICY", "PRIDE", "PRIMA", "PRIME", "PRIMP", "PRINT", "PRION", "PRIOR", "PRISE", "PRISM", "PRIVY", "PRIZE", "PROBE", "PROMO", "PRONE", "PRONG", "PROOF", "PROSE", "PROUD", "PROVE", "PROXY", "PRUDE", "PRUNE", "PUDGY", "PULLS", "PULSE", "PUMPS", "PUNCH", "PUPIL", "PUPPY", "PURSE", "PYLON",
            "QUACK", "QUAIL", "QUALM", "QUARK", "QUART", "QUASI", "QUEEN", "QUELL", "QUERY", "QUEST", "QUEUE", "QUICK", "QUIET", "QUILL", "QUILT", "QUINT", "QUIRK", "QUITE", "QUOTA", "QUOTE",
            "RABBI", "RACED", "RACES", "RADAR", "RADIO", "RAGGY", "RAIDS", "RAILS", "RAINY", "RAISE", "RALLY", "RAMPS", "RANCH", "RANGE", "RANGY", "RANKS", "RAPID", "RATED", "RATES", "RATIO", "RATTY", "RAVEN", "RAZOR", "REACH", "REACT", "READS", "READY", "REALM", "REARM", "REBEL", "RECAP", "RECON", "RECTO", "REDLY", "REEDY", "REFER", "REHAB", "REIGN", "REINS", "RELAX", "RELAY", "RELIC", "REMIT", "REMIX", "RENAL", "RENEW", "RENTS", "REPAY", "REPLY", "RESIN", "RESTS", "RETRO", "REUSE", "RHINO", "RHYME", "RIDER", "RIDGE", "RIFLE", "RIGHT", "RIGID", "RIGOR", "RILED", "RINGS", "RINSE", "RIOTS", "RISEN", "RISES", "RISKS", "RISKY", "RITES", "RITZY", "RIVAL", "RIVEN", "RIVER", "RIVET", "ROADS", "ROAST", "ROBES", "ROBOT", "ROCKS", "ROCKY", "ROGUE", "ROILY", "ROLES", "ROLLS", "ROMAN", "ROOFS", "ROOMS", "ROOMY", "ROOTS", "ROPES", "ROSES", "ROSIN", "ROTOR", "ROUGE", "ROUGH", "ROUND", "ROUTE", "ROVER", "ROYAL", "RUDDY", "RUGBY", "RUINS", "RULED", "RULER", "RULES", "RUMBA", "RUMMY", "RUMOR", "RUNIC", "RUNNY", "RUNTY", "RURAL", "RUSTY",
            "SABLE", "SADLY", "SAFER", "SAGGY", "SAILS", "SAINT", "SALAD", "SALES", "SALLY", "SALON", "SALSA", "SALTS", "SALTY", "SALVE", "SAMBA", "SANDS", "SANDY", "SATED", "SATIN", "SATYR", "SAUCE", "SAUCY", "SAUNA", "SAVED", "SAVER", "SAVES", "SAVOR", "SAVVY", "SCALD", "SCALE", "SCALP", "SCALY", "SCAMP", "SCANT", "SCAPE", "SCARE", "SCARF", "SCARP", "SCARS", "SCARY", "SCENE", "SCENT", "SCHMO", "SCOFF", "SCOLD", "SCONE", "SCOOP", "SCOOT", "SCOPE", "SCORE", "SCORN", "SCOTS", "SCOUR", "SCOUT", "SCRAM", "SCRAP", "SCREW", "SCRIM", "SCRIP", "SCRUB", "SCRUM", "SCUBA", "SEALS", "SEAMS", "SEATS", "SEEDS", "SEEDY", "SEEKS", "SEEMS", "SEGUE", "SEIZE", "SELLS", "SENDS", "SENSE", "SERUM", "SERVE", "SETUP", "SEVEN", "SHADE", "SHADY", "SHAFT", "SHAKE", "SHAKY", "SHALE", "SHALL", "SHAME", "SHANK", "SHAPE", "SHARD", "SHARE", "SHARP", "SHAVE", "SHAWL", "SHEAF", "SHEAR", "SHEEN", "SHEEP", "SHEER", "SHEET", "SHELF", "SHELL", "SHIFT", "SHILL", "SHINE", "SHINY", "SHIPS", "SHIRE", "SHIRT", "SHOCK", "SHOES", "SHONE", "SHOOK", "SHOOT", "SHOPS", "SHORE", "SHORN", "SHORT", "SHOTS", "SHOUT", "SHOVE", "SHOWN", "SHOWS", "SHRUG", "SHUNT", "SHUSH", "SIDES", "SIDLE", "SIEGE", "SIGHT", "SIGIL", "SIGNS", "SILLY", "SILTY", "SINCE", "SINEW", "SINGE", "SINGS", "SINUS", "SITAR", "SITES", "SIXTH", "SIXTY", "SIZED", "SIZES", "SKALD", "SKANK", "SKATE", "SKEIN", "SKIER", "SKIES", "SKIFF", "SKILL", "SKIMP", "SKINS", "SKIRT", "SKULL", "SLABS", "SLAIN", "SLAKE", "SLANG", "SLANT", "SLASH", "SLATE", "SLAVE", "SLEEK", "SLEEP", "SLEET", "SLEPT", "SLICE", "SLIDE", "SLIME", "SLIMY", "SLING", "SLINK", "SLOPE", "SLOSH", "SLOTH", "SLOTS", "SLUMP", "SLUSH", "SLYLY", "SMALL", "SMART", "SMASH", "SMEAR", "SMELL", "SMELT", "SMILE", "SMITE", "SMOKE", "SNAIL", "SNAKE", "SNARE", "SNARL", "SNEER", "SNIDE", "SNIFF", "SNIPE", "SNOOP", "SNORE", "SNORT", "SNOUT", "SOBER", "SOCKS", "SOFTY", "SOGGY", "SOILS", "SOLAR", "SOLID", "SOLVE", "SONAR", "SONGS", "SONIC", "SOOTH", "SORRY", "SORTS", "SOUGH", "SOULS", "SOUND", "SOUTH", "SPACE", "SPADE", "SPAIN", "SPARE", "SPARK", "SPATE", "SPAWN", "SPEAK", "SPEED", "SPELL", "SPEND", "SPENT", "SPIES", "SPINE", "SPLAT", "SPLIT", "SPOIL", "SPOKE", "SPOON", "SPORT", "SPOTS", "SPRAY", "SPURS", "SQUAD", "STACK", "STAFF", "STAGE", "STAIN", "STAIR", "STAKE", "STALE", "STALL", "STAMP", "STAND", "STARE", "STARK", "STARS", "START", "STASH", "STATE", "STAYS", "STEAD", "STEAK", "STEAL", "STEAM", "STEEL", "STEEP", "STEER", "STEMS", "STENO", "STEPS", "STERN", "STICK", "STIFF", "STILE", "STILL", "STILT", "STING", "STINK", "STINT", "STOCK", "STOIC", "STOKE", "STOLE", "STOMP", "STONE", "STONY", "STOOD", "STOOL", "STOOP", "STOPS", "STORE", "STORK", "STORM", "STORY", "STOUT", "STOVE", "STRAP", "STRAW", "STRAY", "STREP", "STREW", "STRIP", "STRUM", "STRUT", "STUCK", "STUDY", "STUFF", "STUMP", "STUNT", "STYLE", "SUAVE", "SUEDE", "SUGAR", "SUITE", "SUITS", "SULLY", "SUNNY", "SUNUP", "SUPER", "SURGE", "SUSHI", "SWALE", "SWAMI", "SWAMP", "SWANK", "SWANS", "SWARD", "SWARM", "SWASH", "SWATH", "SWEAR", "SWEAT", "SWEEP", "SWEET", "SWELL", "SWEPT", "SWIFT", "SWILL", "SWINE", "SWING", "SWIPE", "SWIRL", "SWISH", "SWISS", "SWOON", "SWOOP", "SWORD", "SWORE", "SWORN", "SWUNG",
            "TABLE", "TACIT", "TAFFY", "TAILS", "TAINT", "TAKEN", "TAKES", "TALES", "TALKS", "TALLY", "TALON", "TAMED", "TANGO", "TANGY", "TANKS", "TAPES", "TARDY", "TAROT", "TARRY", "TASKS", "TASTE", "TASTY", "TATTY", "TAUNT", "TAWNY", "TAXED", "TAXES", "TAXIS", "TAXON", "TEACH", "TEAMS", "TEARS", "TEARY", "TEASE", "TECHY", "TEDDY", "TEENS", "TEENY", "TEETH", "TELLS", "TELLY", "TEMPO", "TENDS", "TENOR", "TENSE", "TENTH", "TENTS", "TERMS", "TESTS", "TEXAS", "TEXTS", "THANK", "THEFT", "THEIR", "THEME", "THERE", "THESE", "THETA", "THICK", "THIEF", "THIGH", "THINE", "THING", "THINK", "THIRD", "THONG", "THORN", "THOSE", "THREE", "THREW", "THROW", "THUMB", "TIARA", "TIBIA", "TIDAL", "TIDES", "TIGER", "TIGHT", "TILDE", "TILED", "TILES", "TILTH", "TIMED", "TIMER", "TIMES", "TIMID", "TINES", "TINNY", "TIPSY", "TIRED", "TITLE", "TOAST", "TODAY", "TOKEN", "TOMMY", "TONAL", "TONED", "TONES", "TONGS", "TONIC", "TONNE", "TOOLS", "TOONS", "TOOTH", "TOPAZ", "TOPIC", "TORCH", "TORSO", "TORTE", "TORUS", "TOTAL", "TOTEM", "TOUCH", "TOUGH", "TOURS", "TOWEL", "TOWER", "TOWNS", "TOXIC", "TOXIN", "TRACE", "TRACK", "TRACT", "TRADE", "TRAIL", "TRAIN", "TRAIT", "TRAMP", "TRAMS", "TRASH", "TRAWL", "TRAYS", "TREAD", "TREAT", "TREES", "TREND", "TRIAD", "TRIAL", "TRIBE", "TRICK", "TRIED", "TRIES", "TRIKE", "TRILL", "TRIPS", "TRITE", "TROLL", "TROOP", "TROUT", "TRUCE", "TRUCK", "TRULY", "TRUNK", "TRUST", "TRUTH", "TUBBY", "TUBES", "TULIP", "TUMMY", "TUNED", "TUNES", "TUNIC", "TURKS", "TURNS", "TUTEE", "TUTOR", "TWANG", "TWEAK", "TWICE", "TWINS", "TWIRL", "TWIST", "TYING", "TYPES", "TYRES",
            "UDDER", "ULCER", "ULNAR", "ULTRA", "UMBRA", "UNBAN", "UNCAP", "UNCLE", "UNCUT", "UNDER", "UNDUE", "UNFED", "UNFIT", "UNHIP", "UNIFY", "UNION", "UNITE", "UNITS", "UNITY", "UNLIT", "UNMET", "UNSAY", "UNTIE", "UNTIL", "UNZIP", "UPPER", "UPSET", "URBAN", "URGED", "URINE", "USAGE", "USERS", "USHER", "USING", "USUAL", "UTTER", "UVULA",
            "VAGUE", "VALET", "VALID", "VALOR", "VALUE", "VALVE", "VAPOR", "VAULT", "VAUNT", "VEINS", "VEINY", "VENAL", "VENOM", "VENUE", "VERBS", "VERGE", "VERSE", "VICAR", "VIDEO", "VIEWS", "VIGIL", "VIGOR", "VILLA", "VINES", "VINYL", "VIRAL", "VIRUS", "VISIT", "VISOR", "VITAL", "VIVID", "VIXEN", "VOCAL", "VODKA", "VOGUE", "VOICE", "VOTED", "VOTER", "VOTES", "VOUCH", "VOWED", "VOWEL", "VROOM",
            "WAGES", "WAGON", "WAIST", "WAITS", "WAIVE", "WALKS", "WALLS", "WALTZ", "WANTS", "WARDS", "WARES", "WARNS", "WASTE", "WATCH", "WATER", "WAVED", "WAVES", "WAXEN", "WEARS", "WEARY", "WEAVE", "WEBBY", "WEDGE", "WEEDS", "WEEKS", "WEIGH", "WEIRD", "WELLS", "WELSH", "WETLY", "WHALE", "WHEAT", "WHEEL", "WHERE", "WHICH", "WHILE", "WHINE", "WHISK", "WHITE", "WHOLE", "WHORL", "WHOSE", "WIDEN", "WIDER", "WIDOW", "WIDTH", "WIELD", "WILLS", "WIMPY", "WINCE", "WINCH", "WINDS", "WINDY", "WINES", "WINGS", "WIPED", "WIRED", "WIRES", "WISER", "WITCH", "WITTY", "WIVES", "WOKEN", "WOMAN", "WOMEN", "WOODS", "WORDS", "WORKS", "WORLD", "WORMS", "WORMY", "WORRY", "WORSE", "WORST", "WORTH", "WOULD", "WOUND", "WOVEN", "WRATH", "WRECK", "WRIST", "WRITE", "WRONG", "WROTE", "WRYLY",
            "XENON", "XEROX",
            "YACHT", "YARDS", "YAWNS", "YEARN", "YEARS", "YEAST", "YELLS", "YIELD", "YODEL", "YOUNG", "YOURS", "YOUTH", "YUCCA", "YUMMY",
            "ZEBRA", "ZILCH", "ZINGY", "ZONAL", "ZONES"
        };
    private string _chosenWord;
    private string _solutionWord;
    private int[] _chosenWordIxs = new int[5];

    private int[][] CubeConfigs = new int[6][]
    {
        new int[27] {0, 1, 2, 9, 10, 11, 17, 18, 19, 3, 4, 5, 12, -1, 13, 18, 19, 20, 6, 7, 8, 14, 15, 16, 23, 24, 25 }, // -X to +X, -Y to +Y, -Z to +Z
        new int[27] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, -1, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 }, // -X to +X, -Z to +Z, -Y to +Y
        new int[27] {0, 9, 17, 1, 10, 18, 2, 11, 19, 3, 12, 20, 4, -1, 21, 5, 13, 22, 6, 14, 23, 7, 15, 24, 8, 16, 25 }, // -Y to +Y, -X to +X, -Z to +Z
        new int[27] {0, 9, 17, 3, 12, 20, 6, 14, 23, 1, 10, 18, 4, -1, 21, 7, 15, 24, 2, 11, 19, 5, 13, 22, 8, 16, 25 }, // -Y to +Y, -Z to +Z, -X to +X
        new int[27] {0, 3, 6, 1, 4, 7, 2, 5, 8, 9, 12, 14, 10, -1, 15, 11, 13, 16, 17, 20, 23, 18, 21, 24, 19, 22, 25 }, // -Z to +Z, -X to +X, -Y to +Y
        new int[27] {0, 3, 6, 9, 12, 14, 17, 20, 23, 1, 4, 7, 10, -1, 15, 18, 21, 24, 2, 5, 8, 11, 13, 16, 19, 22, 25 }  // -Z to +Z, -Y to +Y, -X to +X
    };
    private static readonly string[] ConfigNames = { "XYZ", "XZY", "YXZ", "YZX", "ZXY", "ZYX" };
    private static readonly int[][] ConfigIxs = new int[6][] { new int[3] { 1, 5, 25 }, new int[3] { 1, 25, 5 }, new int[3] { 5, 1, 25 }, new int[3] { 5, 25, 1 }, new int[3] { 25, 1, 5 }, new int[3] { 25, 5, 1 } };
    private int _config;
    private int[][] _taps = new int[5][] { new int[3], new int[3], new int[3], new int[3], new int[3] };
    private int[] _chosenTapCode;

    private bool _tapCodeLastCodeStillActive;
    private List<int> _tapCodeInput = new List<int>();
    private Coroutine _waitingToInput;
    private int[] _solutionTapCode;

    private Coroutine _timer;
    private float _elapsedTime;
    private Coroutine _playTapCode;

    private void Start()
    {
        _moduleId = _moduleIdCounter++;
        Sel.OnInteract += SelPress;
        Sel.OnInteractEnded += SelRelease;

        // START RULE SEED
        var rnd = RuleSeedable.GetRNG();
        var wordList = WordList.ToArray();
        rnd.Next();
        rnd.ShuffleFisherYates(wordList);
        // END RULE SEED

        int ix = Rnd.Range(0, 125);
        _chosenWord = wordList[ix];
        Debug.LogFormat("[3D Tap Code #{0}] The chosen word is {1}.", _moduleId, _chosenWord);
        _config = Rnd.Range(0, 6);
        for (int i = 0; i < _chosenWord.Length; i++)
            _chosenWordIxs[i] = Array.IndexOf(CubeConfigs[_config], _chosenWord[i] - 'A');
        Debug.LogFormat("[3D Tap Code #{0}] The coordinate order is {1}.", _moduleId, ConfigNames[_config]);
        _chosenTapCode = _chosenWord.SelectMany(ltr =>
        {
            var index = Array.IndexOf(CubeConfigs[_config], ltr - 'A');
            return new[] { index % 3 + 1, index / 3 % 3 + 1, index / 9 + 1 };
        }).ToArray();
        var _tapCodeChosenLog = _chosenTapCode.Join("");
        Debug.LogFormat("[3D Tap Code #{0}] Chosen word in 3D Tap Code: {1}.", _moduleId, Enumerable.Range(0, 5).Select(ltrIx => _tapCodeChosenLog.Substring(3 * ltrIx, 3)).Join(" "));
        var SerialNumber = BombInfo.GetSerialNumber();
        var snNums = new int[3];
        for (int i = 0; i < snNums.Length; i++)
            snNums[i] = SerialNumber[i] - '0' <= 9 ? SerialNumber[i] - '0' : SerialNumber[i] - 'A' + 1;
        for (int i = 0; i < ConfigIxs[_config].Length; i++)
        {
            var c = ConfigIxs[_config][i];
            var below = ix % c;
            var inf = ix / c % 5;
            var above = ix / (5 * c);
            inf = (inf + snNums[i]) % 5;
            ix = below + c * inf + (5 * c) * above;
        }
        _solutionWord = wordList[ix];
        Debug.LogFormat("[3D Tap Code #{0}] Solution word: {1}", _moduleId, _solutionWord);
        _solutionTapCode = _solutionWord.SelectMany(ltr =>
        {
            var index = Array.IndexOf(CubeConfigs[_config], ltr - 'A');
            return new[] { index % 3 + 1, index / 3 % 3 + 1, index / 9 + 1 };
        }).ToArray();
        var tapCodeSolLog = _solutionTapCode.Join("");
        Debug.LogFormat("[3D Tap Code #{0}] Solution in 3D Tap Code: {1}.", _moduleId, Enumerable.Range(0, 5).Select(ltrIx => tapCodeSolLog.Substring(3 * ltrIx, 3)).Join(" "));
    }

    private bool SelPress()
    {
        if (!_moduleSolved)
            _timer = StartCoroutine(Timer());
        return false;
    }

    private void SelRelease()
    {
        if (!_moduleSolved)
        {
            if (_timer != null)
                StopCoroutine(_timer);
            if (_playTapCode != null)
                StopCoroutine(_playTapCode);
            if (_elapsedTime < 0.5f)
            {
                if (_tapCodeLastCodeStillActive)
                    _tapCodeInput[_tapCodeInput.Count - 1]++;
                else
                {
                    _tapCodeLastCodeStillActive = true;
                    _tapCodeInput.Add(1);
                }
                if (_waitingToInput != null)
                    StopCoroutine(_waitingToInput);
                _waitingToInput = StartCoroutine(acknowledgeTapCode());
            }
            else
            {
                _tapCodeInput.Clear();
                _playTapCode = StartCoroutine(PlayTapCode());
            }
        }
    }

    private IEnumerator PlayTapCode()
    {
        for (int i = 0; i < _chosenTapCode.Length; i++)
        {
            for (int j = 0; j < _chosenTapCode[i]; j++)
            {
                Audio.PlaySoundAtTransform("Tap", transform);
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator Timer()
    {
        _elapsedTime = 0f;
        while (true)
        {
            yield return null;
            _elapsedTime += Time.deltaTime;
        }
    }

    private IEnumerator acknowledgeTapCode()
    {
        yield return new WaitForSeconds(1f);
        Audio.PlaySoundAtTransform("MiniTap", transform);
        _tapCodeLastCodeStillActive = false;

        if (_tapCodeInput.Count < 15)
            yield break;

        interpretTapCode();
    }

    private void interpretTapCode()
    {
        if (_waitingToInput != null)
        {
            StopCoroutine(_waitingToInput);
            _waitingToInput = null;
        }

        if (_solutionTapCode.SequenceEqual(_tapCodeInput))
        {
            _moduleSolved = true;
            Module.HandlePass();
            Debug.LogFormat("[3D Tap Code #{0}] Submitted {1}. Module solved.", _moduleId, _solutionWord);
        }
        else
        {
            Module.HandleStrike();
            Debug.LogFormat("[3D Tap Code #{0}] Inputted {1}. Strike.", _moduleId, _tapCodeInput.Join(""));
        }
        _tapCodeInput = null;
    }
}