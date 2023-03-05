namespace Lab1.RayTracer;
class Array<T>
{
    public Array(ulong size)
    {
        this.size = 0;
        this.capacity = 2 * size;
        this.array = new T[capacity];
    }
    public Array(int size) : this((ulong)size) {}
    public Array() : this(0) {}

    private T[] array;
    private ulong capacity;
    public ulong size {get; private set; }

    public void append(T value)
    {
        if (size == capacity)
        {
            capacity *= 2;
            T[] newArray = new T[capacity];

            for (ulong i = 0; i < size; i++)
            {
                newArray[i] = this.array[i];
            }
            this.array = newArray;
        }

        this.array[size] = value;
        size++;
    }
    
    public T remove(ulong idx)
    {
        if (idx < 0 || idx > size - 1)
        {
            throw new IndexOutOfRangeException();
        }

        T value = this.array[idx];
        
        for (ulong i = idx+1; i < size; i++)
        {
            this.array[i-1] = this.array[i];
        }

        size--;
        
        return value;
    }
    
    public T this[int idx]
    {
        get
        {
            if (idx < 0 || idx > (int)size - 1)
            {
                throw new IndexOutOfRangeException();
            }
            return this.array[idx];
        }
    }
    public T this[ulong idx]
    {
        get
        {
            if (idx < 0 || idx > size - 1)
            {
                throw new IndexOutOfRangeException();
            }
            return this.array[idx];
        }
    }
}