export const filterOption = (input: string, option?: any) =>
  option?.children.toLowerCase().indexOf(input.toLowerCase()) >= 0;
