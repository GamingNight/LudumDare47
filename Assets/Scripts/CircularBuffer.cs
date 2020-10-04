using System;
using System.Diagnostics;

public class CircularBuffer<T>
{

    private int capacity;
    private int size;
    private T[] array;
    private int head;
    private int tail;

    public CircularBuffer(int capacity) {
        this.capacity = capacity;
        this.array = new T[capacity];
        this.head = 0;
        this.tail = 0;
        this.size = 0;
    }

    public void InitWithValue(T value) {
        for (int i = 0; i < array.Length; i++) {
            array[i] = value;
        }
        size = capacity;
    }

    public void Clear() {
        head = 0;
        tail = 0;
        size = 0;
    }

    public bool IsEmpty() {
        return size == 0;
    }

    public bool isFull() {
        return size == capacity;
    }

    public int GetCapacity() {
        return capacity;
    }

    public int GetSize() {
        return size;
    }

    public void Add(params T[] newValues) {
        for (int i = 0; i < newValues.Length; i++) {
            array[head] = newValues[i];
            head = (head + 1) % capacity;
            size++;
            if (size > capacity)
                throw new IndexOutOfRangeException("Can't add anymore, CircularBuffer is full!");
        }
    }

    public T Read() {
        int _tail = tail;
        tail = (tail + 1) % capacity;
        size--;
        if (size < 0)
            throw new IndexOutOfRangeException("Can't read anymore, CircularBuffer is empty!");
        return array[_tail];
    }

    public T[] ReadAll() {
        T[] res = ReadAllNoDelete();
        Clear();
        return res;
    }

    public T ReadNoDelete(int index) {
        if (index >= capacity || index < 0)
            throw new IndexOutOfRangeException("Incorrect index: " + index);
        if (IsEmpty())
            throw new IndexOutOfRangeException("Nothing to read, buffer is empty!");

        int actualIndex = (tail + index) % capacity;

        if (head > tail && (actualIndex < tail || actualIndex >= head))
            throw new IndexOutOfRangeException("Can't read data, out of bounds! Index = " + index);
        else if (head < tail && actualIndex >= head && actualIndex < tail)
            throw new IndexOutOfRangeException("Can't read data, out of bounds! Index = " + index);
        return array[(tail + index) % capacity];
    }

    public T[] ReadAllNoDelete() {
        T[] res = new T[size];
        if (size > 0)
            if (head > tail) {
                Array.Copy(array, tail, res, 0, size);
                return res;
            } else {
                Array.Copy(array, tail, res, 0, capacity - tail);
                Array.Copy(array, 0, res, capacity - tail, head);
            }
        return res;
    }
}
