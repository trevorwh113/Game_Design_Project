using Unity.VisualScripting;

public interface IDataService
{
    // saves data; returns true if data saved successful
    bool SavaData<T>(string RelativePath, T Data, bool Encrypted );

    // loads data
    T LoadData<T>(string RelativePath, bool Encrypted);
}

