using Newtonsoft.Json;

public class WordModel
{
    [JsonProperty("wordRank")]
    public int WordRank { get; set; }

    [JsonProperty("headWord")]
    public string HeadWord { get; set; }

    [JsonProperty("content")]
    public Content Content { get; set; }

    [JsonProperty("bookId")]
    public string BookId { get; set; }
}

public class Content
{
    [JsonProperty("word")]
    public Word Word { get; set; }
}

public class Word
{
    [JsonProperty("wordHead")]
    public string WordHead { get; set; }

    [JsonProperty("wordId")]
    public string WordId { get; set; }

    [JsonProperty("content")]
    public WordContent Content { get; set; }
}

public class WordContent
{
    [JsonProperty("sentence")]
    public SentenceContent Sentence { get; set; }

    [JsonProperty("usphone")]
    public string Usphone { get; set; }

    [JsonProperty("ukphone")]
    public string Ukphone { get; set; }

    [JsonProperty("ukspeech")]
    public string Ukspeech { get; set; }

    [JsonProperty("star")]
    public int Star { get; set; }

    [JsonProperty("syno")]
    public Synonym Syno { get; set; }

    [JsonProperty("phrase")]
    public PhraseContent Phrase { get; set; }

    [JsonProperty("phone")]
    public string Phone { get; set; }

    [JsonProperty("speech")]
    public string Speech { get; set; }

    [JsonProperty("remMethod")]
    public RemMethod RemMethod { get; set; }

    [JsonProperty("relWord")]
    public RelWord RelWord { get; set; }

    [JsonProperty("usspeech")]
    public string Usspeech { get; set; }

    [JsonProperty("trans")]
    public List<Translation> Trans { get; set; }
}

public class SentenceContent
{
    [JsonProperty("sentences")]
    public List<Sentence> Sentences { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}

public class Sentence
{
    [JsonProperty("sContent")]
    public string SContent { get; set; }

    [JsonProperty("sCn")]
    public string SCn { get; set; }
}

public class Synonym
{
    [JsonProperty("synos")]
    public List<Syno> Synos { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}

public class Syno
{
    [JsonProperty("pos")]
    public string Pos { get; set; }

    [JsonProperty("tran")]
    public string Tran { get; set; }

    [JsonProperty("hwds")]
    public List<Hwd> Hwds { get; set; }
}

public class Hwd
{
    [JsonProperty("w")]
    public string W { get; set; }
}

public class PhraseContent
{
    [JsonProperty("phrases")]
    public List<Phrase> Phrases { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}

public class Phrase
{
    [JsonProperty("pContent")]
    public string PContent { get; set; }

    [JsonProperty("pCn")]
    public string PCn { get; set; }
}

public class RemMethod
{
    [JsonProperty("val")]
    public string Val { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}

public class RelWord
{
    [JsonProperty("rels")]
    public List<Rel> Rels { get; set; }

    [JsonProperty("desc")]
    public string Desc { get; set; }
}

public class Rel
{
    [JsonProperty("pos")]
    public string Pos { get; set; }

    [JsonProperty("words")]
    public List<WordItem> Words { get; set; }
}

public class WordItem
{
    [JsonProperty("hwd")]
    public string Hwd { get; set; }

    [JsonProperty("tran")]
    public string Tran { get; set; }
}

public class Translation
{
    [JsonProperty("tranCn")]
    public string TranCn { get; set; }

    [JsonProperty("descOther")]
    public string DescOther { get; set; }

    [JsonProperty("descCn")]
    public string DescCn { get; set; }

    [JsonProperty("pos")]
    public string Pos { get; set; }

    [JsonProperty("tranOther")]
    public string TranOther { get; set; }
}