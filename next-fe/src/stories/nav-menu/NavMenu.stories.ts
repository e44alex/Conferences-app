import { Meta, StoryObj } from "@storybook/react";
import { NavMenu } from "../../components/nav-menu/NavMenu";

const meta: Meta<typeof NavMenu> = {
  title: "Components/NavMenu",
  component: NavMenu,
  tags: ["autodocs"],
  argTypes: {},
};

export default meta;

type Story = StoryObj<typeof NavMenu>;

export const Default: Story = {
  args: {},
};
